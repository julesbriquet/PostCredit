using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
public class CommandUICreator : MonoBehaviour {

    public Image upImage;
    public Image downImage;
    public Image leftImage;
    public Image rightImage;
    public Image actionImage;


    public List<Image> imageCommandList;

    public void GenerateUICommand(string commandList)
    {

        int imageWidth = (int)upImage.rectTransform.rect.width;
        Debug.Log(imageWidth);

        int startPosition = -(imageWidth / 2) * (commandList.Length - 1);
        int currentPosition = startPosition;

        RectTransform currentRectTransf = this.GetComponent<RectTransform>();

        for (int i = 0; i < commandList.Length; i++)
        {
            Image instanciateImg = null;
            Debug.Log(commandList);

            if (commandList[i] == 'U')
                instanciateImg = Instantiate(upImage, new Vector3(currentPosition, 0, 0), Quaternion.identity) as Image;
            if (commandList[i] == 'D')
                instanciateImg = Instantiate(downImage, new Vector3(currentPosition, 0, 0), Quaternion.identity) as Image;
            if (commandList[i] == 'L')
                instanciateImg = Instantiate(leftImage, new Vector3(currentPosition, 0, 0), Quaternion.identity) as Image;
            if (commandList[i] == 'R')
                instanciateImg = Instantiate(rightImage, new Vector3(currentPosition, 0, 0), Quaternion.identity) as Image;
            if (commandList[i] == 'A')
                instanciateImg = Instantiate(actionImage, new Vector3(currentPosition, 0, 0), Quaternion.identity) as Image;


            if (instanciateImg != null)
            {
                Debug.Log(currentPosition);
                instanciateImg.rectTransform.SetParent(this.transform, false);

                imageCommandList.Add(instanciateImg);
                //instanciateImg.rectTransform.position = new Vector3(currentPosition, instanciateImg.rectTransform.position.y, instanciateImg.rectTransform.position.z);
            }

            currentPosition += imageWidth;
            
        }

    }

    public void CommandUINoticed(bool isGoodCommand, int index)
    {
        if (isGoodCommand)
            imageCommandList[index].color = Color.green;
        else
            imageCommandList[index].color = Color.red;
    }
}
