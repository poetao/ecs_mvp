using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;

public class WebClientAdapter : CrossBindingAdaptor
    {
#if !UNITY_IOS
        static CrossBindingMethodInfo<System.Net.WriteStreamClosedEventArgs> mOnWriteStreamClosed_0 = new CrossBindingMethodInfo<System.Net.WriteStreamClosedEventArgs>("OnWriteStreamClosed");
#endif
        static CrossBindingFunctionInfo<System.Uri, System.Net.WebRequest> mGetWebRequest_1 = new CrossBindingFunctionInfo<System.Uri, System.Net.WebRequest>("GetWebRequest");
        static CrossBindingFunctionInfo<System.Net.WebRequest, System.Net.WebResponse> mGetWebResponse_2 = new CrossBindingFunctionInfo<System.Net.WebRequest, System.Net.WebResponse>("GetWebResponse");
        static CrossBindingFunctionInfo<System.Net.WebRequest, System.IAsyncResult, System.Net.WebResponse> mGetWebResponse_3 = new CrossBindingFunctionInfo<System.Net.WebRequest, System.IAsyncResult, System.Net.WebResponse>("GetWebResponse");
        static CrossBindingMethodInfo<System.Net.OpenReadCompletedEventArgs> mOnOpenReadCompleted_4 = new CrossBindingMethodInfo<System.Net.OpenReadCompletedEventArgs>("OnOpenReadCompleted");
        static CrossBindingMethodInfo<System.Net.OpenWriteCompletedEventArgs> mOnOpenWriteCompleted_5 = new CrossBindingMethodInfo<System.Net.OpenWriteCompletedEventArgs>("OnOpenWriteCompleted");
        static CrossBindingMethodInfo<System.Net.DownloadStringCompletedEventArgs> mOnDownloadStringCompleted_6 = new CrossBindingMethodInfo<System.Net.DownloadStringCompletedEventArgs>("OnDownloadStringCompleted");
        static CrossBindingMethodInfo<System.Net.DownloadDataCompletedEventArgs> mOnDownloadDataCompleted_7 = new CrossBindingMethodInfo<System.Net.DownloadDataCompletedEventArgs>("OnDownloadDataCompleted");
        static CrossBindingMethodInfo<System.ComponentModel.AsyncCompletedEventArgs> mOnDownloadFileCompleted_8 = new CrossBindingMethodInfo<System.ComponentModel.AsyncCompletedEventArgs>("OnDownloadFileCompleted");
        static CrossBindingMethodInfo<System.Net.UploadStringCompletedEventArgs> mOnUploadStringCompleted_9 = new CrossBindingMethodInfo<System.Net.UploadStringCompletedEventArgs>("OnUploadStringCompleted");
        static CrossBindingMethodInfo<System.Net.UploadDataCompletedEventArgs> mOnUploadDataCompleted_10 = new CrossBindingMethodInfo<System.Net.UploadDataCompletedEventArgs>("OnUploadDataCompleted");
        static CrossBindingMethodInfo<System.Net.UploadFileCompletedEventArgs> mOnUploadFileCompleted_11 = new CrossBindingMethodInfo<System.Net.UploadFileCompletedEventArgs>("OnUploadFileCompleted");
        static CrossBindingMethodInfo<System.Net.UploadValuesCompletedEventArgs> mOnUploadValuesCompleted_12 = new CrossBindingMethodInfo<System.Net.UploadValuesCompletedEventArgs>("OnUploadValuesCompleted");
        static CrossBindingMethodInfo<System.Net.DownloadProgressChangedEventArgs> mOnDownloadProgressChanged_13 = new CrossBindingMethodInfo<System.Net.DownloadProgressChangedEventArgs>("OnDownloadProgressChanged");
        static CrossBindingMethodInfo<System.Net.UploadProgressChangedEventArgs> mOnUploadProgressChanged_14 = new CrossBindingMethodInfo<System.Net.UploadProgressChangedEventArgs>("OnUploadProgressChanged");
        static CrossBindingFunctionInfo<System.Boolean> mget_CanRaiseEvents_15 = new CrossBindingFunctionInfo<System.Boolean>("get_CanRaiseEvents");
        static CrossBindingMethodInfo<System.EventHandler> madd_Disposed_16 = new CrossBindingMethodInfo<System.EventHandler>("add_Disposed");
        static CrossBindingMethodInfo<System.EventHandler> mremove_Disposed_17 = new CrossBindingMethodInfo<System.EventHandler>("remove_Disposed");
        static CrossBindingFunctionInfo<System.ComponentModel.ISite> mget_Site_18 = new CrossBindingFunctionInfo<System.ComponentModel.ISite>("get_Site");
        static CrossBindingMethodInfo<System.ComponentModel.ISite> mset_Site_19 = new CrossBindingMethodInfo<System.ComponentModel.ISite>("set_Site");
        static CrossBindingMethodInfo mDispose_20 = new CrossBindingMethodInfo("Dispose");
        static CrossBindingMethodInfo<System.Boolean> mDispose_21 = new CrossBindingMethodInfo<System.Boolean>("Dispose");
        static CrossBindingFunctionInfo<System.Type, System.Object> mGetService_22 = new CrossBindingFunctionInfo<System.Type, System.Object>("GetService");
#if !UNITY_IOS
        static CrossBindingFunctionInfo<System.Type, System.Runtime.Remoting.ObjRef> mCreateObjRef_23 = new CrossBindingFunctionInfo<System.Type, System.Runtime.Remoting.ObjRef>("CreateObjRef");
#endif
        static CrossBindingFunctionInfo<System.Object> mGetLifetimeService_24 = new CrossBindingFunctionInfo<System.Object>("GetLifetimeService");
        static CrossBindingFunctionInfo<System.Object> mInitializeLifetimeService_25 = new CrossBindingFunctionInfo<System.Object>("InitializeLifetimeService");
    public override Type BaseCLRType
    {
        get
        {
            return typeof(System.Net.WebClient);
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(Adapter);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adapter(appdomain, instance);
    }

    public class Adapter : System.Net.WebClient, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        public Adapter()
        {

        }

        public Adapter(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }

        public ILTypeInstance ILInstance { get { return instance; } }

            //protected override void OnWriteStreamClosed(System.Net.WriteStreamClosedEventArgs e)
            //{
            //    if (mOnWriteStreamClosed_0.CheckShouldInvokeBase(this.instance))
            //        base.OnWriteStreamClosed(e);
            //    else
            //        mOnWriteStreamClosed_0.Invoke(this.instance, e);
            //}

            protected override System.Net.WebRequest GetWebRequest(System.Uri address)
            {
                if (mGetWebRequest_1.CheckShouldInvokeBase(this.instance))
                    return base.GetWebRequest(address);
                else
                    return mGetWebRequest_1.Invoke(this.instance, address);
            }

            protected override System.Net.WebResponse GetWebResponse(System.Net.WebRequest request)
            {
                if (mGetWebResponse_2.CheckShouldInvokeBase(this.instance))
                    return base.GetWebResponse(request);
                else
                    return mGetWebResponse_2.Invoke(this.instance, request);
            }

            protected override System.Net.WebResponse GetWebResponse(System.Net.WebRequest request, System.IAsyncResult result)
            {
                if (mGetWebResponse_3.CheckShouldInvokeBase(this.instance))
                    return base.GetWebResponse(request, result);
                else
                    return mGetWebResponse_3.Invoke(this.instance, request, result);
            }

            protected override void OnOpenReadCompleted(System.Net.OpenReadCompletedEventArgs e)
            {
                if (mOnOpenReadCompleted_4.CheckShouldInvokeBase(this.instance))
                    base.OnOpenReadCompleted(e);
                else
                    mOnOpenReadCompleted_4.Invoke(this.instance, e);
            }

            protected override void OnOpenWriteCompleted(System.Net.OpenWriteCompletedEventArgs e)
            {
                if (mOnOpenWriteCompleted_5.CheckShouldInvokeBase(this.instance))
                    base.OnOpenWriteCompleted(e);
                else
                    mOnOpenWriteCompleted_5.Invoke(this.instance, e);
            }

            protected override void OnDownloadStringCompleted(System.Net.DownloadStringCompletedEventArgs e)
            {
                if (mOnDownloadStringCompleted_6.CheckShouldInvokeBase(this.instance))
                    base.OnDownloadStringCompleted(e);
                else
                    mOnDownloadStringCompleted_6.Invoke(this.instance, e);
            }

            protected override void OnDownloadDataCompleted(System.Net.DownloadDataCompletedEventArgs e)
            {
                if (mOnDownloadDataCompleted_7.CheckShouldInvokeBase(this.instance))
                    base.OnDownloadDataCompleted(e);
                else
                    mOnDownloadDataCompleted_7.Invoke(this.instance, e);
            }

            protected override void OnDownloadFileCompleted(System.ComponentModel.AsyncCompletedEventArgs e)
            {
                if (mOnDownloadFileCompleted_8.CheckShouldInvokeBase(this.instance))
                    base.OnDownloadFileCompleted(e);
                else
                    mOnDownloadFileCompleted_8.Invoke(this.instance, e);
            }

            protected override void OnUploadStringCompleted(System.Net.UploadStringCompletedEventArgs e)
            {
                if (mOnUploadStringCompleted_9.CheckShouldInvokeBase(this.instance))
                    base.OnUploadStringCompleted(e);
                else
                    mOnUploadStringCompleted_9.Invoke(this.instance, e);
            }

            protected override void OnUploadDataCompleted(System.Net.UploadDataCompletedEventArgs e)
            {
                if (mOnUploadDataCompleted_10.CheckShouldInvokeBase(this.instance))
                    base.OnUploadDataCompleted(e);
                else
                    mOnUploadDataCompleted_10.Invoke(this.instance, e);
            }

            protected override void OnUploadFileCompleted(System.Net.UploadFileCompletedEventArgs e)
            {
                if (mOnUploadFileCompleted_11.CheckShouldInvokeBase(this.instance))
                    base.OnUploadFileCompleted(e);
                else
                    mOnUploadFileCompleted_11.Invoke(this.instance, e);
            }

            protected override void OnUploadValuesCompleted(System.Net.UploadValuesCompletedEventArgs e)
            {
                if (mOnUploadValuesCompleted_12.CheckShouldInvokeBase(this.instance))
                    base.OnUploadValuesCompleted(e);
                else
                    mOnUploadValuesCompleted_12.Invoke(this.instance, e);
            }

            protected override void OnDownloadProgressChanged(System.Net.DownloadProgressChangedEventArgs e)
            {
                if (mOnDownloadProgressChanged_13.CheckShouldInvokeBase(this.instance))
                    base.OnDownloadProgressChanged(e);
                else
                    mOnDownloadProgressChanged_13.Invoke(this.instance, e);
            }

            protected override void OnUploadProgressChanged(System.Net.UploadProgressChangedEventArgs e)
            {
                if (mOnUploadProgressChanged_14.CheckShouldInvokeBase(this.instance))
                    base.OnUploadProgressChanged(e);
                else
                    mOnUploadProgressChanged_14.Invoke(this.instance, e);
            }

        //public override void add_Disposed(System.EventHandler value)
        //{
        //    if (madd_Disposed_16.CheckShouldInvokeBase(this.instance))
        //        base.add_Disposed(value);
        //    else
        //        madd_Disposed_16.Invoke(this.instance, value);
        //}

        //public override void remove_Disposed(System.EventHandler value)
        //{
        //    if (mremove_Disposed_17.CheckShouldInvokeBase(this.instance))
        //        base.remove_Disposed(value);
        //    else
        //        mremove_Disposed_17.Invoke(this.instance, value);
        //}

        //public override void Dispose()
        //{
        //    if (mDispose_20.CheckShouldInvokeBase(this.instance))
        //        base.Dispose();
        //    else
        //        mDispose_20.Invoke(this.instance);
        //}

        protected override void Dispose(System.Boolean disposing)
            {
                if (mDispose_21.CheckShouldInvokeBase(this.instance))
                    base.Dispose(disposing);
                else
                    mDispose_21.Invoke(this.instance, disposing);
            }

            protected override System.Object GetService(System.Type service)
            {
                if (mGetService_22.CheckShouldInvokeBase(this.instance))
                    return base.GetService(service);
                else
                    return mGetService_22.Invoke(this.instance, service);
            }

#if !UNITY_IOS
            public override System.Runtime.Remoting.ObjRef CreateObjRef(System.Type requestedType)
            {
                if (mCreateObjRef_23.CheckShouldInvokeBase(this.instance))
                    return base.CreateObjRef(requestedType);
                else
                    return mCreateObjRef_23.Invoke(this.instance, requestedType);
            }
#endif

            //public override System.Object GetLifetimeService()
            //{
            //    if (mGetLifetimeService_24.CheckShouldInvokeBase(this.instance))
            //        return base.GetLifetimeService();
            //    else
            //        return mGetLifetimeService_24.Invoke(this.instance);
            //}

            public override System.Object InitializeLifetimeService()
            {
                if (mInitializeLifetimeService_25.CheckShouldInvokeBase(this.instance))
                    return base.InitializeLifetimeService();
                else
                    return mInitializeLifetimeService_25.Invoke(this.instance);
            }

            protected override System.Boolean CanRaiseEvents
            {
            get
            {
                if (mget_CanRaiseEvents_15.CheckShouldInvokeBase(this.instance))
                    return base.CanRaiseEvents;
                else
                    return mget_CanRaiseEvents_15.Invoke(this.instance);

            }
            }

            public override System.ComponentModel.ISite Site
            {
            get
            {
                if (mget_Site_18.CheckShouldInvokeBase(this.instance))
                    return base.Site;
                else
                    return mget_Site_18.Invoke(this.instance);

            }
            set
            {
                if (mset_Site_19.CheckShouldInvokeBase(this.instance))
                    base.Site = value;
                else
                    mset_Site_19.Invoke(this.instance, value);

            }
            }

        public override string ToString()
        {
            IMethod m = appdomain.ObjectType.GetMethod("ToString", 0);
            m = instance.Type.GetVirtualMethod(m);
            if (m == null || m is ILMethod)
            {
                return instance.ToString();
            }
            else
                return instance.Type.FullName;
        }
    }
}

