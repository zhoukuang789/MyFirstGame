using ProjectBase.SingletonBase;
using UnityEngine;

namespace Airplane.Weapon {
    public class PlaneWeaponControllerService : Singletonable<PlaneWeaponControllerService> {
        
        //------------------field ---------------------------------------
        private PlaneBehaviour plane;
        
        private PlaneWeaponBehaviour planeWeapon;

        
        //-------------------------getter & setter ----------------------
        public PlaneWeaponControllerService SetPlane(PlaneBehaviour plane) {
            this.plane = plane;
            planeWeapon = plane.GetPlaneWeapon();
            return this;
        }
        
        
        // ---------------------function -------------------------------

        public void Fire() {
            if (planeWeapon.GetCooldownTime() <= 0f) {
                ShootABullet();
                planeWeapon.ResetCooldownTime();
            }
        }

        private void ShootABullet() {
            foreach (Transform muzzleTransform in planeWeapon.muzzleTransformList) {
                // 生成子弹
                GameObject bullet = GameObject.Instantiate(planeWeapon.GetBulletPrefab(), muzzleTransform.position,
                    muzzleTransform.rotation);
                // 初始化子弹属性
                bullet.transform.LookAt(plane.transform.position + plane.transform.forward * planeWeapon.GetBulletRange());
                bullet.GetComponent<Bullet.BulletBehaviour>()
                    .SetCamp(plane.camp)
                    .SetDamage(planeWeapon.GetDamage())
                    .SetRange(planeWeapon.GetBulletRange())
                    .SetInitialSpeed(planeWeapon.GetBulletInitialSpeed())
                    .SetFlightSpeed(planeWeapon.GetBulletFlightSpeed());
                // 激活子弹
                bullet.SetActive(true);
            }
        }
    }
}