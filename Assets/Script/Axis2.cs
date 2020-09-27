using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis2 : MonoBehaviour
{
    float angleUp = 60f;
    float angleDown = -10f;

    [SerializeField] GameObject Player;

    [SerializeField] Camera cam;

    [SerializeField] float rotate_speed = 3;

    //Axisの位置を指定する変数
    [SerializeField] Vector3 axisPos;

    //マウススクロールの値を入れる
    [SerializeField] float scroll;

    //マウスホイールの値を保存
    [SerializeField] float scrolling;

    // Start is called before the first frame update
    void Start()
    {
        //CameraのAxisに相対的な位置をlocalPositionで指定
        cam.transform.localPosition = new Vector3(0, 1, -4);
        //CameraとAxisの向きを最初だけそろえる
        cam.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Axisの位置をPlayerの位置+axisPosで決める
        transform.position = Player.transform.position + axisPos;
        //三人称の時のCameraの位置にマウススクロールの値を足して位置を調整

        //マウススクロールの値を入れる
        //scroll += Input.GetAxis("Mouse ScrollWheel");

        //マウススクロールの値は動かさないと０になるのでここで保存する
        //scrolling += Input.GetAxis("Mouse ScrollWheel");

        //Cameraの位置、ｚ軸にスクロール分を加える
        //cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, cam.transform.localPosition.z + scroll);

        //Cameraの角度にマウスからとった値を入れる
        transform.eulerAngles += new Vector3(Input.GetAxis("Vertical1") * rotate_speed, Input.GetAxis("Horizontal1") * rotate_speed, 0);

        //X軸の角度
        float angleX = transform.eulerAngles.x;

        if (angleX >= 180)
        {
            angleX = angleX - 360;
        }

        //Math.Clamp(値,最小値,最大値)でX軸の値を制限する
        transform.eulerAngles = new Vector3(Mathf.Clamp(angleX, angleDown, angleUp), transform.eulerAngles.y, transform.eulerAngles.z);
        
    }
}
