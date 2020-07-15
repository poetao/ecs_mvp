# 开发环境

[Unity 2019.2.4f1](https://unity.cn/releases/lts)

[Rider           ](https://www.jetbrains.com/rider/download/#section=windows)


---

## 框架

- Assets/script/Framework:

  框架源码

- Assets/Scripts/Game:

  游戏源码

---

## 游戏

### 目录结构

#### 资源列表

游戏资源列表，loading 阶段会预先加载资源。

> Assets/Scripts/Game/Vendor/Resource.cs

#### Presenter

MVP 的 Presenter，表现层的控制器，类似 MVC 的 Controller。

> Assets/Scripts/Game/Presenter/Game.cs

> Assets/Scripts/Game/Presenter/Home.cs

> Assets/Scripts/Game/Presenter/Loading.cs

> Assets/Scripts/Game/Presenter/Login.cs

> Assets/Scripts/Game/Presenter/Stages/Level1.cs

> Assets/Scripts/Game/Presenter/Stages/Level2.cs


#### View

MVP 的 View，通过声明的形式将 Model 转换成 Unity Component可理解的数据。
每个 View 通过Proxy组件挂载到Inspector面板，在Inspector面板中指定数据所要绑定Component的GameObject。

> Assets/Scripts/Game/View/Game.cs

> Assets/Scripts/Game/View/Home.cs

> Assets/Scripts/Game/View/Loading.cs

> Assets/Scripts/Game/View/Login.cs

> Assets/Scripts/Game/View/Stages/Level1.cs

> Assets/Scripts/Game/View/Stages/Level2.cs

#### Component

MVP 的 View，直接继承MonoBehaviour，通常用于生命周期相关或复杂的表现等需要直接调用Unity API的地方。

> Assets/Scripts/Framework/Components/Windows/Container.cs

#### Features

MVP 的 Model，处理游戏的业务逻辑。

> Assets/Scripts/Game/Feature/Account.cs

> Assets/Scripts/Game/Feature/Game.cs.cs

> Assets/Scripts/Game/Feature/Stage.cs.cs

> Assets/Scripts/Game/Feature/User.cs

### 框架结构

框架在 MVP 的基础上加入了 DataBinding 机制：

#### Model

具有 get，set，subscribe 等接口的类，如 Assets/Scripts/Framework/Core/State，或者包含State的类。

因为有 DataBinding 机制，所以**不能直接修改** `get` 返回的数据，必须构造新的数据后再用 `set` 更新，例如

```C#
var key = "answer";
state.Set(key, state.Get<int>(key) + 1);

```

#### Presenter

Presenter 持有 Model，这些 Model 可以是 Presenter 默认创建的 state，也可以是 Feature 等的实例。

加载场景或Prefab时，框架会根据路径创建关联的 Presenter。

#### View

View 将关联的 Model 转换成 表现层 可理解的数据接口（Link），并能够把输入（Slot）转发到关联的 Presenter。

View 通过 Proxy 里指定路径的方式 挂载到场景或Prefab，通过Link和Slot标签导出到Inspector面板，并关联对应GameObject和Model中的数据

#### DataBinding

框架会根据 View 的数据接口获得的数据类型和关联的GameObject中的组件类型自动更新组件，例如：

> * 数据是 string，组件是 Text，更新 Text 的 text
> * 数据是 string，组件是 Animation，播放 Animation 的 clip
> * 数据是 string，组件是 Animator，执行 Animator 的 trigger
> * 数据是 boolean，组件是 Button，更新 Button 的 interactable
> * 数据是 boolean，组件是 GameObject，更新 GameObject 的 active

具体规则参考 `Assets/Scripts/Framework/Views/Link`。

### 源码解析

#### Assets/Scripts/Game/Presenter/Login.cs

```C#
namespace Presenters
{
    using Builder = MVP.Framework.Bootstraps.Components.Context;

    public class Login : Presenter
    {
        private Scene   scene;
        private Window  window;
        private Account account;

        protected override void Create(Context context, Builder builder, params object[] args)
        {
            base.Create(context, builder, args);
            scene       = builder.GetManager<Scene>();
            window		= builder.GetManager<Window>();
            account     = builder.GetFeature<Account>();

            Subscrible();
        }

        private void Subscrible()
        {
            // 订阅Account中AccountData的数据
            var observable = account.GetObservable("");
            observable.Subscribe(x =>
            {
                var data = x.Value<AccountData>();
                state.Set("name", data.nickName);
            });
        }

        private void DoLogin()
        {
            if (account.IsLogin()) account.Logout();

            var data = new AccountData();
            data.id = 1;
            data.nickName = $"TEST_{DateTime.Now.Ticks}";
            account.LocalLogin(data);
            state.Set("isLogin", true);
        }

        private async void SwitchToHome()
        {
            await scene.Run("Home");
        }
    }
}
```

创建登录场景时框架自动创建 Presenter，Presenter 订阅 Model `Assets/Scripts/Game/Feature/Account.cs`。

#### Assets/Scripts/Game/View/Login.cs

```C#
namespace Views
{
    // 关联Presenter中state的name字段
    [Link("name")]

    // 关联View中的enableLogin函数的返回值，eanbleLogin函数的参数通过名字关联Presenter中的isLogin字段
    [Link("enableLogin")]

    // 关联View中的enableEnter函数的返回值，eanbleEnter函数的参数通过名字关联Presenter中的isLogin字段
    [Link("enableEnter")]

    // 关联Presenter中的DoLogin函数
    [Slot("DoLogin")]

    // 关联Presenter中的SwitchToHome函数
    [Slot("SwitchToHome")]

    public class Login : View
    {
        public bool enableLogin(bool isLogin) { return !isLogin; }
        public bool enableEnter(bool isLogin) { return isLogin; }
    }
}
```

## 其它

* DataBinding 只会更新一个关联组件，对不同组件类型有先后顺序，如果一个 GameObject 有 Text 又有 Animation，将会优先更新 Text。具体规则参考 `Assets/Scripts/Framework/Views/Link`。

## 引用插件

* [UniRx           ](https://github.com/neuecc/UniRx)

* [AsyncAwaitUtil  ](https://assetstore.unity.com/packages/tools/integration/async-await-support-101056)

* [TexturePacker   ](https://www.codeandweb.com/texturepacker/unity)

* [JsonDotNet      ](https://assetstore.unity.com/packages/tools/input-management/json-net-for-unity-11347)
