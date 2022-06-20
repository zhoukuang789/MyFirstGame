using ProjectBase.SingletonBase;
using UnityEngine;

namespace Plane.Weapon {
    public class PlaneWeaponControllerService : Singletonable<PlaneWeaponControllerService> {
        private PlaneWeaponBehaviour planeWeapon;

        public PlaneWeaponControllerService SetPlaneWeapon(PlaneWeaponBehaviour planeWeapon) {
            this.planeWeapon = planeWeapon;
            return this;
        }

        public void Fire() {
            if (planeWeapon.GetCooldownTime() <= 0f) {
                ShootABullet();
                planeWeapon.ResetCooldownTime();
            }
        }

        private void ShootABullet() {
            // 准心位置在三维世界空间中的坐标
            Camera main = Camera.main;
            Vector3 aimingPositionInWorldPoint =
                main.ScreenToWorldPoint(new Vector3(main.pixelWidth / 2f, main.pixelHeight / 2f, 150f));
            foreach (Transform muzzleTransform in planeWeapon.muzzleTransformList) {
                // 生成子弹
                GameObject bullet = GameObject.Instantiate(planeWeapon.bulletPrefab, muzzleTransform.position,
                    muzzleTransform.rotation);
                // 初始化子弹属性
                bullet.transform.LookAt(aimingPositionInWorldPoint);
                bullet.GetComponent<Bullet.BulletBehaviour>()
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