<!--
 * @Author: 谭兴洋
 * @Date: 2020-12-14 19:28:25
 * @LastEditTime: 2020-12-17 14:23:46
 * @LastEditors: Please set LastEditors
 * @Description: In User Settings Edit
 * @FilePath: \2D_Game\README.MD
-->

# Unity2D项目，飞针小游戏

————————————————————————————————————————

## 这个项目是我学习unity3d的过程中做的一个小游戏，这份文档将对步骤进行比较详细的说明

 ###如果你想研究这个项目，可以fork一个分支或者clone整个项目。
————————————————————————————————————————
————————————————————————————————————————

### 一、准备阶段

1. 安装unity编辑器，这里我使用的是2020.1.13f1c1版本,如果是其他版本，可能会出现一些小问题。你可以从这里下载[unity](https://unity.cn/releases/full/2020) 的安装包,我建议使用hub进行安装。***_另：注意请安装至非中文路径_***

![unity各版本](./picture/unity.jpg "unity版本选择")
2. 安装完成后打开UnityHub,然后登录，可以使用微信登录，登陆后，激活许可证，说明如下：

![激活许可证](./picture/115927.jpg)

 ####选择激活新许可证

![激活新许可证](./picture/120437.jpg)

1. 安装IDE，你可以使用VS，即visualstudio进行C#的编写，这里我使用的是JetBrain的Rider，所以不再对软件的安装进行说明，（个人觉得Rider比visualstudio好用，但Rider是付费平台，所以有感兴趣的自行~~找破解版~~购买  :)
2. 接下来下载项目

![下载项目](./picture/120725.jpg)

![下载项目](./picture/120754.jpg)

下载完成后解压至***_非中文路径_***
5. 导入项目

点击项目，导入项目，然后选择你刚刚解压的文件夹

![导入项目](./picture/121457.jpg)
导入后你就会发现，可以打开啦

### 二、场景搭建

 ###场景部分比较简单，我将不会详细说明，你也可以直接用我的场景，如果对场景有疑问，可以创建issue,或者邮件联系我。（以后或许我会更新这一部分的文档说明）

## 三、脚本编写

 脚本环节我将会重点描述

1. ### 首先创建球体的自转脚本

 声明一个`public`权限类型的2D刚体，用来绑定球体，实现自转。`float`类型的`speed`变量，记录球体自转速度。创建`GameController`类型的对象，用以访问`GameController`类的方法，并在`start()`方法中初始化。***注意：`start()`方法为加载时执行，`Update()`方法为每帧执行，所以初始化对象应写在`start()`方法里面，而自转代码应写在`Update()`方法里面***

### 代码如下

```C#
    public Rigidbody2D rb;
    public float speed = 100;
    private GameController _gameController;
    
    private void Start()
    {
        //初始化对象
        var gameControllerObject = GameObject.FindWithTag("MainCamera");    //GameController类的脚本挂在摄像机上，所以此处为MainCamera
        _gameController = gameControllerObject.GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();   //h获取刚体属性
    }

    private void Update()
    {
        //如果游戏状态为结束则不再自转，此处为调用的GameController类变量，overOnOff为布尔值，用来判断游戏是否处于结束状态
        if (! _gameController.overOnOff)
        {
            //球体自转代码，自转为Z轴自转，所以X和Y轴值为0
            transform.Rotate (0,0, speed * Time.deltaTime);
        }    
    }
 ```

# 再鸽一鸽 :)
