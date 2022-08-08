using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;


namespace Tencent
{
    class IMNative
    {
        //tools
        public static IntPtr StrToUtf8HGlobalPtr(string str)
        {
            if(str == null)
            {
                return IntPtr.Zero;
            }
            byte[] datas = System.Text.Encoding.UTF8.GetBytes(str);
            IntPtr dataPtr = Marshal.AllocHGlobal(datas.Length + 1);
            Marshal.Copy(datas, 0, dataPtr, datas.Length);
            Marshal.WriteByte(dataPtr, datas.Length, 0);
            return dataPtr;
        }
        public static string Utf8HGlobalPtrToStr(IntPtr data)
        {
            if(IntPtr.Zero.Equals(data))
            {
                return null;
            }
            return Marshal.PtrToStringUTF8(data);
        }
        //callbacks
        //[System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)] 告知不要释放代理函数内存如IntPtr占用（改内存应该是C/C++DLL给到的）
        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMRecvNewMsgCallback(IntPtr json_msg_array, IntPtr user_data);

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMMsgReadedReceiptCallback(IntPtr json_msg_readed_receipt_array, IntPtr user_data);

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMMsgRevokeCallback(IntPtr json_msg_locator_array, IntPtr user_data);

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMMsgElemUploadProgressCallback(IntPtr json_msg, UInt32 index, UInt32 cur_size, UInt32 total_size, IntPtr user_data);

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMGroupTipsEventCallback(IntPtr json_group_tip_array, IntPtr user_data);

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMConvEventCallback(Int32 conv_event, IntPtr user_data);

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMNetworkStatusListenerCallback(Int32 status, Int32 code, IntPtr desc, IntPtr user_data);

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMKickedOfflineCallback(IntPtr user_data);

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMUserSigExpiredCallback(IntPtr user_data);

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMOnAddFriendCallback(IntPtr json_identifier_array, IntPtr user_data);

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMOnDeleteFriendCallback(IntPtr json_identifier_array, IntPtr user_data);

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMUpdateFriendProfileCallback(IntPtr json_friend_profile_update_array, IntPtr user_data);

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMFriendAddRequestCallback(IntPtr json_friend_add_request_pendency_array, IntPtr user_data);

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMLogCallback(Int32 level, IntPtr log, IntPtr user_data);

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMMsgUpdateCallback(Int32 json_msg_array, IntPtr user_data);

        [System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute(System.Runtime.InteropServices.CallingConvention.Cdecl)]
        public delegate void TIMCommCallback(Int32 code, IntPtr desc, IntPtr json_param, IntPtr ptr);

        //functions - set callbacks
        /// <summary>
        /// 增加接收新消息回调。
        /// </summary>
        /// <param name="cb">新消息回调函数，请参考 TIMRecvNewMsgCallback</param>
        /// <param name="user_data">用户自定义数据，IM SDK 只负责传回给回调函数 cb，不做任何处理</param>
        [DllImport(@"imsdk.dll")]
        public extern static void TIMAddRecvNewMsgCallback(TIMRecvNewMsgCallback cb, IntPtr user_data);
        /// <summary>
        /// 删除接收新消息回调。
        /// </summary>
        /// <param name="cb">新消息回调函数，请参考 TIMRecvNewMsgCallback</param>
        [DllImport(@"imsdk.dll")]
        public extern static void TIMRemoveRecvNewMsgCallback(TIMRecvNewMsgCallback cb);
        /// <summary>
        /// 设置消息已读回执回调。
        /// </summary>
        /// <param name="cb">消息已读回执回调，请参考 TIMMsgReadedReceiptCallback</param>
        /// <param name="user_data">用户自定义数据，IM SDK 只负责传回给回调函数 cb，不做任何处理</param>
        [DllImport(@"imsdk.dll")]
        public extern static void TIMSetMsgReadedReceiptCallback(TIMMsgReadedReceiptCallback cb, IntPtr user_data);
        /// <summary>
        /// 设置接收的消息被撤回回调。
        /// </summary>
        /// <param name="cb">消息撤回通知回调，请参考 TIMMsgRevokeCallback</param>
        /// <param name="user_data">用户自定义数据，IM SDK 只负责传回给回调函数 cb，不做任何处理</param>
        [DllImport(@"imsdk.dll")]
        public extern static void TIMSetMsgRevokeCallback(TIMMsgRevokeCallback cb, IntPtr user_data);
        /// <summary>
        /// 设置消息内元素相关文件上传进度回调。
        /// </summary>
        /// <param name="cb">文件上传进度回调，请参考 TIMMsgElemUploadProgressCallback</param>
        /// <param name="user_data">用户自定义数据，IM SDK 只负责传回给回调函数 cb，不做任何处理</param>
        [DllImport(@"imsdk.dll")]
        public extern static void TIMSetMsgElemUploadProgressCallback(TIMMsgElemUploadProgressCallback cb, IntPtr user_data);
        /// <summary>
        /// 群属性变更回调。废弃（无任何引用）
        /// </summary>
        /// <param name="group_id">群 ID</param>
        /// <param name="json_group_attibute_array">群提示列表</param>
        /// <param name="user_data">用户自定义数据，IM SDK 只负责传回给回调函数 cb，不做任何处理</param>
        public delegate void TIMGroupAttributeChangedCallback(IntPtr group_id, IntPtr json_group_attibute_array, IntPtr user_data);

        /// <summary>
        /// 设置群组系统消息回调
        /// </summary>
        /// <param name="cb">群消息回调，请参考 TIMGroupTipsEventCallback</param>
        /// <param name="user_data">用户自定义数据，IM SDK 只负责传回给回调函数 cb，不做任何处理</param>
        [DllImport(@"imsdk.dll")]
        public extern static void TIMSetGroupTipsEventCallback(TIMGroupTipsEventCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static void TIMSetConvEventCallback(TIMConvEventCallback cb, IntPtr user_data);

        /// <summary>
        /// 会话未读消息总数变化。废弃（无任何引用）
        /// </summary>
        /// <param name="total_unread_count">会话未读消息总数变化。</param>
        /// <param name="user_data">会话未读消息总数变化。</param>
        public delegate void TIMConvTotalUnreadMessageCountChangedCallback(Int32 total_unread_count, IntPtr user_data);

        /// <summary>
        /// 设置网络连接状态监听回调。
        /// 当调用接口 TIMInit 时，IM SDK 会去连接云后台。此接口设置的回调用于监听网络连接的状态。<br/>
        /// 网络连接状态包含四个：正在连接、连接失败、连接成功、已连接。这里的网络事件不表示用户本地网络状态，仅指明 IM SDK 是否与即时通信 IM 云 Server 连接状态。<br/>
        /// 可选设置，如果要用户感知是否已经连接服务器，需要设置此回调，用于通知调用者跟通讯后台链接的连接和断开事件，另外，如果断开网络，等网络恢复后会自动重连，自动拉取消息通知用户，用户无需关心网络状态，仅作通知之用。<br/>
        /// 只要用户处于登录状态，IM SDK 内部会进行断网重连，用户无需关心。<br/>
        /// </summary>
        /// <param name="cb">连接事件回调，请参考 TIMNetworkStatusListenerCallback</param>
        /// <param name="user_data">用户自定义数据，IM SDK 只负责传回给回调函数 cb，不做任何处理</param>
        [DllImport(@"imsdk.dll")]
        public extern static void TIMSetNetworkStatusListenerCallback(TIMNetworkStatusListenerCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static void TIMSetKickedOfflineCallback(TIMKickedOfflineCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static void TIMSetUserSigExpiredCallback(TIMUserSigExpiredCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static void TIMSetOnAddFriendCallback(TIMOnAddFriendCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static void TIMSetOnDeleteFriendCallback(TIMOnDeleteFriendCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static void TIMSetUpdateFriendProfileCallback(TIMUpdateFriendProfileCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static void TIMSetFriendAddRequestCallback(TIMFriendAddRequestCallback cb, IntPtr user_data);

        //[DllImport(@"imsdk.dll")]
        //public extern static void TIMFriendApplicationListDeletedCallback(IntPtr json_identifier_array, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static void TIMSetLogCallback(TIMLogCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static void TIMSetMsgUpdateCallback(TIMMsgUpdateCallback cb, IntPtr user_data);

        //functions 
        [DllImport(@"imsdk.dll")]
        public extern static int TIMLogin(IntPtr user_id, IntPtr user_sig, IntPtr cb, IntPtr user_data);

        public static int TIMLogin(string user_id, string user_sig, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(user_id);
            IntPtr ptrUserSig = StrToUtf8HGlobalPtr(user_sig);
            IntPtr cbFuncPtr = Marshal.GetFunctionPointerForDelegate<IMNative.TIMCommCallback>(cb);
            int iRet = TIMLogin(ptrParam, ptrUserSig, cbFuncPtr, user_data);
            Marshal.FreeHGlobal(ptrParam);
            Marshal.FreeHGlobal(ptrUserSig);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMLogout(TIMCommCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGetLoginStatus();

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGetLoginUserID(IntPtr user_id_buffer);

        [DllImport(@"imsdk.dll")]
        public extern static int TIMConvCreate(IntPtr conv_id, Int32 conv_type, TIMCommCallback cb, IntPtr user_data);

        public static int TIMConvCreate(string conv_id, Int32 conv_type, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            int iRet = TIMConvCreate(ptrParam, conv_type, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMConvDelete(IntPtr conv_id, Int32 conv_type, TIMCommCallback cb, IntPtr user_data);

        public static int TIMConvDelete(string conv_id,Int32 conv_type, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            int iRet = TIMConvDelete(ptrParam, conv_type, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMConvGetConvList(TIMCommCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static int TIMConvGetConvInfo(IntPtr json_get_conv_list_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMConvGetConvInfo(string conv_id, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            int iRet = TIMConvGetConvInfo(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMConvSetDraft(IntPtr conv_id, Int32 conv_type, IntPtr json_draft_param);

        public static int TIMConvSetDraft(string conv_id, Int32 conv_type,string json_get_conv_list_param)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            IntPtr ptrDraftParam = StrToUtf8HGlobalPtr(json_get_conv_list_param);
            int iRet = TIMConvSetDraft(ptrParam, conv_type, ptrDraftParam);
            Marshal.FreeHGlobal(ptrParam);
            Marshal.FreeHGlobal(ptrDraftParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMConvCancelDraft(IntPtr conv_id, Int32 conv_type);

        public static int TIMConvCancelDraft(string conv_id, Int32 conv_type)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            int iRet = TIMConvCancelDraft(ptrParam, conv_type);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMConvPinConversation(IntPtr conv_id, Int32 conv_type, bool is_pinned, TIMCommCallback cb, IntPtr user_data);

        public static int TIMConvPinConversation(string conv_id, Int32 conv_type, bool is_pinned, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            int iRet = TIMConvPinConversation(ptrParam, conv_type, is_pinned, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMConvGetTotalUnreadMessageCount(TIMCommCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgSendMessage(IntPtr conv_id, Int32 conv_type, IntPtr json_msg_param, IntPtr message_id_buffer, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgSendMessage(string conv_id, Int32 conv_type, string json_msg_param, IntPtr message_id_buffer, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            IntPtr ptrMsgParam = StrToUtf8HGlobalPtr(json_msg_param);
            int iRet = TIMMsgSendMessage(ptrParam, conv_type, ptrMsgParam, message_id_buffer, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            Marshal.FreeHGlobal(ptrMsgParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgSendNewMsg(IntPtr conv_id, int conv_type, IntPtr json_msg_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgSendNewMsg(string conv_id, Int32 conv_type, string json_msg_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            IntPtr ptrMsgParam = StrToUtf8HGlobalPtr(json_msg_param);
            int iRet = TIMMsgSendNewMsg(ptrParam, conv_type, ptrMsgParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            Marshal.FreeHGlobal(ptrMsgParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgCancelSend(IntPtr conv_id, Int32 conv_type, IntPtr message_id, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgCancelSend(string conv_id, Int32 conv_type, string message_id, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            IntPtr ptrMsgId = StrToUtf8HGlobalPtr(message_id);
            int iRet = TIMMsgCancelSend(ptrParam, conv_type, ptrMsgId, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            Marshal.FreeHGlobal(ptrMsgId);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgFindMessages(IntPtr json_message_id_array, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgFindMessages(string json_message_id_array,TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_message_id_array);
            int iRet = TIMMsgFindMessages(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgReportReaded(IntPtr conv_id, Int32 conv_type, IntPtr json_msg_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgReportReaded(string conv_id, Int32 conv_type, string json_msg_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            IntPtr ptrMsgArray = StrToUtf8HGlobalPtr(json_msg_param);
            int iRet = TIMMsgReportReaded(ptrParam, conv_type, ptrMsgArray, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            Marshal.FreeHGlobal(ptrMsgArray);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgMarkAllMessageAsRead(TIMCommCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgRevoke(IntPtr conv_id, Int32 conv_type, IntPtr json_msg_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgRevoke(string conv_id, Int32 conv_type, string json_msg_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            IntPtr ptrMsgArray = StrToUtf8HGlobalPtr(json_msg_param);
            int iRet = TIMMsgRevoke(ptrParam, conv_type, ptrMsgArray, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            Marshal.FreeHGlobal(ptrMsgArray);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgFindByMsgLocatorList(IntPtr conv_id, Int32 conv_type, IntPtr json_msg_Locator_array, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgFindByMsgLocatorList(string conv_id, Int32 conv_type, string json_msg_Locator_array, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            IntPtr ptrMsgArray = StrToUtf8HGlobalPtr(json_msg_Locator_array);
            int iRet = TIMMsgFindByMsgLocatorList(ptrParam, conv_type, ptrMsgArray, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            Marshal.FreeHGlobal(ptrMsgArray);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgImportMsgList(IntPtr conv_id, Int32 conv_type, IntPtr json_msg_array, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgImportMsgList(string conv_id, Int32 conv_type, string json_msg_array, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            IntPtr ptrMsgArray = StrToUtf8HGlobalPtr(json_msg_array);
            int iRet = TIMMsgImportMsgList(ptrParam, conv_type, ptrMsgArray, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            Marshal.FreeHGlobal(ptrMsgArray);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgSaveMsg(IntPtr conv_id, Int32 conv_type, IntPtr json_msg_array, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgSaveMsg(string conv_id, Int32 conv_type, string json_msg_array, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            IntPtr ptrMsgArray = StrToUtf8HGlobalPtr(json_msg_array);
            int iRet = TIMMsgSaveMsg(ptrParam, conv_type, ptrMsgArray, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            Marshal.FreeHGlobal(ptrMsgArray);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgGetMsgList(IntPtr conv_id, Int32 conv_type, IntPtr json_get_msg_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgGetMsgList(string conv_id, Int32 conv_type, string json_get_msg_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            IntPtr ptrMsgArray = StrToUtf8HGlobalPtr(json_get_msg_param);
            int iRet = TIMMsgGetMsgList(ptrParam, conv_type, ptrMsgArray, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            Marshal.FreeHGlobal(ptrMsgArray);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgDelete(IntPtr conv_id, Int32 conv_type, IntPtr json_msgdel_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgDelete(string conv_id, Int32 conv_type, string json_msgdel_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            IntPtr ptrMsgArray = StrToUtf8HGlobalPtr(json_msgdel_param);
            int iRet = TIMMsgDelete(ptrParam, conv_type, ptrMsgArray, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            Marshal.FreeHGlobal(ptrMsgArray);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgListDelete(IntPtr conv_id, Int32 conv_type, IntPtr json_msg_array, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgListDelete(string conv_id, Int32 conv_type, string json_msg_array, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            IntPtr ptrMsgArray = StrToUtf8HGlobalPtr(json_msg_array);
            int iRet = TIMMsgListDelete(ptrParam, conv_type, ptrMsgArray, cb, user_data);
            Marshal.FreeHGlobal(ptrParam); 
            Marshal.FreeHGlobal(ptrMsgArray);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgClearHistoryMessage(IntPtr conv_id, Int32 conv_type, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgClearHistoryMessage(string conv_id, Int32 conv_type, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(conv_id);
            int iRet = TIMMsgClearHistoryMessage(ptrParam, conv_type, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgSetC2CReceiveMessageOpt(IntPtr json_identifier_array, Int32 opt, Int32 conv_type, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgSetC2CReceiveMessageOpt(string json_identifier_array, Int32 opt, Int32 conv_type, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_identifier_array);
            int iRet = TIMMsgSetC2CReceiveMessageOpt(ptrParam, opt, conv_type, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgGetC2CReceiveMessageOpt(IntPtr json_identifier_array, Int32 conv_type, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgGetC2CReceiveMessageOpt(string json_identifier_array, Int32 conv_type, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_identifier_array);
            int iRet = TIMMsgGetC2CReceiveMessageOpt(ptrParam, conv_type, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgSetGroupReceiveMessageOpt(IntPtr group_id, Int32 opt, Int32 conv_type, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgSetGroupReceiveMessageOpt(string group_id, Int32 opt, Int32 conv_type, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(group_id);
            int iRet = TIMMsgSetGroupReceiveMessageOpt(ptrParam, opt, conv_type, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgDownloadElemToPath(IntPtr json_download_elem_param, IntPtr path, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgDownloadElemToPath(string json_download_elem_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_download_elem_param);
            int iRet = TIMMsgSearchLocalMessages(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgDownloadMergerMessage(IntPtr json_single_msg, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgDownloadMergerMessage(string json_single_msg, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_single_msg);
            int iRet = TIMMsgSearchLocalMessages(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgSearchLocalMessages(IntPtr json_search_message_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgSearchLocalMessages(string json_search_message_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_search_message_param);
            int iRet = TIMMsgSearchLocalMessages(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgSetLocalCustomData(IntPtr json_msg_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgSetLocalCustomData(string json_msg_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_msg_param);
            int iRet = TIMGroupCreate(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMMsgBatchSend(IntPtr json_batch_send_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMMsgBatchSend(string json_batch_send_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_batch_send_param);
            int iRet = TIMGroupCreate(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupCreate(IntPtr json_group_create_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupCreate(string json_group_create_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_group_create_param);
            int iRet = TIMGroupCreate(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupDelete(IntPtr group_id, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupDelete(string group_id, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(group_id);
            int iRet = TIMGroupDelete(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupJoin(IntPtr group_id, IntPtr hello_msg, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupJoin(string group_id, string hello_msg, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(group_id);
            IntPtr ptrHelloMsg = StrToUtf8HGlobalPtr(hello_msg);
            int iRet = TIMGroupJoin(ptrParam, ptrHelloMsg, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            Marshal.FreeHGlobal(ptrHelloMsg);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupQuit(IntPtr group_id, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupQuit(string group_id, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(group_id);
            int iRet = TIMGroupQuit(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupInviteMember(IntPtr json_group_invite_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupInviteMember(string json_group_invite_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_group_invite_param);
            int iRet = TIMGroupInviteMember(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupDeleteMember(IntPtr json_group_delete_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupDeleteMember(string json_group_delete_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_group_delete_param);
            int iRet = TIMGroupDeleteMember(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupGetJoinedGroupList(TIMCommCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupGetGroupInfoList(IntPtr json_group_getinfo_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupGetGroupInfoList(string json_group_getinfo_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_group_getinfo_param);
            int iRet = TIMGroupGetGroupInfoList(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupModifyGroupInfo(IntPtr json_group_modifyinfo_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupModifyGroupInfo(string json_group_modifyinfo_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_group_modifyinfo_param);
            int iRet = TIMGroupModifyGroupInfo(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupGetMemberInfoList(IntPtr json_group_getmeminfos_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupGetMemberInfoList(string json_group_getmeminfos_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_group_getmeminfos_param);
            int iRet = TIMGroupGetMemberInfoList(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupModifyMemberInfo(IntPtr json_group_getmeminfos_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupModifyMemberInfo(string json_group_getmeminfos_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_group_getmeminfos_param);
            int iRet = TIMGroupModifyMemberInfo(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupGetPendencyList(IntPtr json_group_getpendence_list_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupGetPendencyList(string json_group_getpendence_list_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_group_getpendence_list_param);
            int iRet = TIMGroupGetPendencyList(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupReportPendencyReaded(UInt64 time_stamp, TIMCommCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupHandlePendency(UInt64 time_stamp, TIMCommCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupGetOnlineMemberCount(IntPtr groupid, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupGetOnlineMemberCount(string groupid, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(groupid);
            int iRet = TIMGroupGetOnlineMemberCount(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupSearchGroups(IntPtr json_group_search_groups_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupSearchGroups(string json_group_search_groups_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_group_search_groups_param);
            int iRet = TIMGroupSearchGroups(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupSearchGroupMembers(IntPtr json_group_search_group_members_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupSearchGroupMembers(string group_id, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrGroupId = StrToUtf8HGlobalPtr(group_id);
            int iRet = TIMGroupSearchGroupMembers(ptrGroupId, cb, user_data);
            Marshal.FreeHGlobal(ptrGroupId);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupInitGroupAttributes(IntPtr group_id, IntPtr json_group_atrributes, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupInitGroupAttributes(string group_id, string json_group_atrributes, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrGroupId = StrToUtf8HGlobalPtr(group_id);
            IntPtr ptrGroupAtrributes = StrToUtf8HGlobalPtr(json_group_atrributes);
            int iRet = TIMGroupInitGroupAttributes(ptrGroupId, ptrGroupAtrributes, cb, user_data);
            Marshal.FreeHGlobal(ptrGroupId);
            Marshal.FreeHGlobal(ptrGroupAtrributes);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupSetGroupAttributes(IntPtr group_id, IntPtr json_group_atrributes, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupSetGroupAttributes(string group_id, string json_group_atrributes, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrGroupId = StrToUtf8HGlobalPtr(group_id);
            IntPtr ptrGroupAtrributes = StrToUtf8HGlobalPtr(json_group_atrributes);
            int iRet = TIMGroupSetGroupAttributes(ptrGroupId, ptrGroupAtrributes, cb, user_data);
            Marshal.FreeHGlobal(ptrGroupId);
            Marshal.FreeHGlobal(ptrGroupAtrributes);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupDeleteGroupAttributes(IntPtr group_id, IntPtr json_keys, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupDeleteGroupAttributes(string group_id, string json_keys, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrGroupId = StrToUtf8HGlobalPtr(group_id);
            IntPtr ptrKeys = StrToUtf8HGlobalPtr(json_keys);
            int iRet = TIMGroupDeleteGroupAttributes(ptrGroupId, ptrKeys, cb, user_data);
            Marshal.FreeHGlobal(ptrGroupId);
            Marshal.FreeHGlobal(ptrKeys);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMGroupGetGroupAttributes(IntPtr group_id, IntPtr json_keys, TIMCommCallback cb, IntPtr user_data);

        public static int TIMGroupGetGroupAttributes(string group_id, string json_keys, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrGroupId = StrToUtf8HGlobalPtr(group_id);
            IntPtr ptrKeys = StrToUtf8HGlobalPtr(json_keys);
            int iRet = TIMGroupGetGroupAttributes(ptrGroupId, ptrKeys, cb, user_data);
            Marshal.FreeHGlobal(ptrGroupId);
            Marshal.FreeHGlobal(ptrKeys);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMProfileGetUserProfileList(IntPtr json_get_user_profile_list_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMProfileGetUserProfileList(string json_get_user_profile_list_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_get_user_profile_list_param);
            int iRet = TIMProfileGetUserProfileList(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMProfileModifySelfUserProfile(IntPtr json_modify_self_user_profile_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMProfileModifySelfUserProfile(string json_modify_self_user_profile_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_modify_self_user_profile_param);
            int iRet = TIMProfileModifySelfUserProfile(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipGetFriendProfileList(TIMCommCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipAddFriend(IntPtr json_add_friend_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMFriendshipAddFriend(string json_add_friend_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_add_friend_param);
            int iRet = TIMFriendshipAddFriend(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipHandleFriendAddRequest(IntPtr json_handle_friend_add_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMFriendshipHandleFriendAddRequest(string json_handle_friend_add_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_handle_friend_add_param);
            int iRet = TIMFriendshipHandleFriendAddRequest(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipModifyFriendProfile(IntPtr json_modify_friend_info_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMFriendshipModifyFriendProfile(string json_modify_friend_info_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_modify_friend_info_param);
            int iRet = TIMFriendshipModifyFriendProfile(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipDeleteFriend(IntPtr json_delete_friend_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMFriendshipDeleteFriend(string json_delete_friend_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_delete_friend_param);
            int iRet = TIMFriendshipDeleteFriend(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipCheckFriendType(IntPtr json_check_friend_list_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMFriendshipCheckFriendType(string json_check_friend_list_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_check_friend_list_param);
            int iRet = TIMFriendshipCheckFriendType(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipCreateFriendGroup(IntPtr json_create_friend_group_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMFriendshipCreateFriendGroup(string json_create_friend_group_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_create_friend_group_param);
            int iRet = TIMFriendshipCreateFriendGroup(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipGetFriendGroupList(IntPtr json_get_friend_group_list_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMFriendshipGetFriendGroupList(string json_get_friend_group_list_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_get_friend_group_list_param);
            int iRet = TIMFriendshipGetFriendGroupList(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipModifyFriendGroup(IntPtr json_modify_friend_group_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMFriendshipModifyFriendGroup(string json_modify_friend_group_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_modify_friend_group_param);
            int iRet = TIMFriendshipModifyFriendGroup(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipDeleteFriendGroup(IntPtr json_delete_friend_group_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMFriendshipDeleteFriendGroup(string json_delete_friend_group_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_delete_friend_group_param);
            int iRet = TIMFriendshipDeleteFriendGroup(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipAddToBlackList(IntPtr json_add_to_blacklist_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMFriendshipAddToBlackList(string json_add_to_blacklist_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_add_to_blacklist_param);
            int iRet = TIMFriendshipAddToBlackList(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipGetBlackList(TIMCommCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipDeleteFromBlackList(IntPtr json_delete_from_blacklist_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMFriendshipDeleteFromBlackList(string json_delete_from_blacklist_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_delete_from_blacklist_param);
            int iRet = TIMFriendshipDeleteFromBlackList(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipGetPendencyList(IntPtr json_get_pendency_list_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMFriendshipGetPendencyList(string json_get_pendency_list_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_get_pendency_list_param);
            int iRet = TIMFriendshipGetPendencyList(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipDeletePendency(IntPtr json_delete_pendency_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMFriendshipDeletePendency(string json_delete_pendency_param, TIMCommCallback cb, IntPtr user_data) 
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_delete_pendency_param);
            int iRet = TIMFriendshipDeletePendency(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipReportPendencyReaded(UInt64 time_stamp, TIMCommCallback cb, IntPtr user_data);

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipSearchFriends(IntPtr json_search_friends_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMFriendshipSearchFriends(string json_search_friends_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_search_friends_param);
            int iRet = TIMFriendshipSearchFriends(ptrParam, cb, user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMFriendshipGetFriendsInfo(IntPtr json_get_friends_info_param, TIMCommCallback cb, IntPtr user_data);

        public static int TIMFriendshipGetFriendsInfo(string json_get_friends_info_param, TIMCommCallback cb, IntPtr user_data)
        {
            IntPtr ptrParam = StrToUtf8HGlobalPtr(json_get_friends_info_param);
            int iRet = TIMFriendshipGetFriendsInfo(ptrParam, cb,user_data);
            Marshal.FreeHGlobal(ptrParam);
            return iRet;
        }

        [DllImport(@"imsdk.dll")]
        public extern static int TIMInit(UInt64 sdk_app_id, IntPtr json_sdk_config);

        public static int TIMInit(UInt64 sdk_app_id, string json_sdk_config)
        {
            IntPtr ptrConfig = StrToUtf8HGlobalPtr(json_sdk_config);
            int iRet = TIMInit(sdk_app_id, ptrConfig);
            Marshal.FreeHGlobal(ptrConfig);
            return iRet;
        }
        [DllImport(@"imsdk.dll")]
        public extern static IntPtr TIMGetSDKVersion();

        public static string TIMGetSDKVersionCSharp() {
            return Utf8HGlobalPtrToStr(TIMGetSDKVersion());
        }

        //[DllImport(@"imsdk.dll")]
        //public extern static UInt64 TIMGetServerTime();

        [DllImport(@"imsdk.dll")]
        public extern static int TIMUninit();

    }

}
