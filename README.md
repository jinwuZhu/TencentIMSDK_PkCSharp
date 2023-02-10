## Tencent IMSDK C#
Tencent IMSDK C# 是一个针对腾讯IMSDK（C接口）的C#版本封装，解决在C#调用的问题 
## 支持的功能
支持IMSDK C 接口的功能调用
## 使用
接口名称和C SDK保持一致，部分接口添加了 CSharp 的后缀，方便使用
```C#
using System;
using Tencent;
namespace HelloWorldApplication
{
   class HelloWorld
   {
      static void Main(string[] args)
      {
         Console.WriteLine("IMVer:{0}",Tencent.TIMGetSDKVersionCSharp());
         Console.ReadKey();
      }
   }
}
```
需要注意的是，回调函数请使用static的函数
```C#
using System;
using Tencent;
namespace HelloWorldApplication
{
   class HelloWorld
   {
      static void LoginCallback(Int32 code, IntPtr desc, IntPtr json_param, IntPtr ptr)
	  {
	     string strCustomData = "";
	     if(!IntPtr.Zero.Equals(data))
		 {
		   strCustomData = Utf8HGlobalPtrToStr(ptr);
		   Marshal.FreeHGlobal(ptr);
		 }
	     string strDesc = Utf8HGlobalPtrToStr(desc);
	     Console.WriteLine("IM LOGIN Code:{0},desc:{1}",code,strDesc);
	  }
      static void Main(string[] args)
      {
		 UInt64 sdkAppID = 0;
	     TIMInit(sdkAppID,IntPtr.Zero);
	     string strCustomData = "Hello Word";
	     TIMLogin("USERID","USERSIGN",LoginCallback,StrToUtf8HGlobalPtr(strCustomData));
		 //TODO Windows Message Loop
		 
		 TIMUninit();
         Console.ReadKey();
      }
   }
}
```
## 安装
1.从官网下载 IMSDK C接口版本。
2.将Tencent.cs加入到你的项目 并且将IMSDK C接口版本的 DLL放到您的程序目录下即可完成安装。
