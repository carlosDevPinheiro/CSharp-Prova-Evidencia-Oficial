using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gcm.Client;
using Android.Util;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Android.Support.V7.App;
using Android.Media;

using Microsoft.WindowsAzure.MobileServices;
using Android.Graphics;
using System.Net;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
//GET_ACCOUNTS is only needed for android versions 4.0.3 and below 
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
namespace ProvaEvidencia.Droid.PushNotification
{
    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE },
     Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
        Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
        Categories = new string[] { "@PACKAGE_NAME@" })]
    public class PushHandlerBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        public static string[] SENDER_IDS = new string[] { "240527649569" };
    }

    [Service]
    public class GcmService : GcmServiceBase
    {
        /************************** Azure Mobile Service Client ***********************************/

        MobileServiceClient client = new MobileServiceClient("http://provaevidencia.azurewebsites.net");

        /************************** Azure Mobile Service Client ***********************************/

        private const string HEADER_PUSH = "data";
        private const string MESSAGE_PUSH = "message";
        private const string MESSAGE_PUSH_TITLE = "title";
        private const string IMAGE_PUSH = "image";
        private const string UNKNOWN_MESSAGE_PUSH = "Unknown message detail";

        private const string GENERIC_PUSH_MESSAGE = "genericMessage";
        private const string BODY_PUSH_MESSAGE = "body";
       


        public static string RegistrationID { get; private set; }

        public GcmService()
            : base(PushHandlerBroadcastReceiver.SENDER_IDS)
        {

        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose("PushHandlerBroadcastReceiver", "GCM Registered: " + registrationId);
            RegistrationID = registrationId;
            var push = client.GetPush(); MainActivity.CurrentActivity.RunOnUiThread(() => Register(push, null));
        }

        public async void Register(Microsoft.WindowsAzure.MobileServices.Push push, IEnumerable<string> tags)
        {
            try
            {
                //const string templateBodyGCM = "{\"data\":{\"message\":\"$(messageParam)\"}}";

                const string templateBodyGCM = "{\"" + HEADER_PUSH + "\":{" +
                        "\"" + MESSAGE_PUSH + "\":\"$(messageParam)\"" +
                        "\"" + MESSAGE_PUSH_TITLE + "\":\"$(titleParam)\"" +
                        "\"" + IMAGE_PUSH + "\":\"$(imageParam)\"" +
                        "}}";

                JObject templates = new JObject();
                templates["genericMessage"] = new JObject
                {
                    { "body", templateBodyGCM }
                };

                await push.RegisterAsync(RegistrationID, templates);

                Log.Info("Push Installation Id", push.InstallationId.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
              // Debugger.Break();
            }
        }




        protected override void OnMessage(Context context, Intent intent)
        {
            Log.Info("PushHandlerBroadcastReceiver", "GCM Message Received!");
            var msg = new StringBuilder();
            if (intent != null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                    msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
            }
            //Store the message 
            var prefs = GetSharedPreferences(context.PackageName, FileCreationMode.Private);
            var edit = prefs.Edit();
            edit.PutString("last_msg", msg.ToString());
            edit.Commit();

            string message = intent.Extras.GetString(MESSAGE_PUSH);
            string title = intent.Extras.GetString(MESSAGE_PUSH_TITLE);
            string image = intent.Extras.GetString(IMAGE_PUSH);

            if (!string.IsNullOrEmpty(message))
            {
                CreateNotification(title,message,image);
               // CreateNotification("Nova Notificação!", "Menssagem: " + title);
               

                return;
            }
            string msg2 = intent.Extras.GetString("msg");
            if (!string.IsNullOrEmpty(msg2))
            {
                CreateNotification("Nova Notificação!!", msg2,null);
                return;
            }
            CreateNotification("Unknown message details", msg.ToString(),null);
        }


        void CreateNotification(string title, string desc, string image)
        {
            //Create notification
            var notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
            //Create an intent to show ui 
            var uiIntent = new Intent(this, typeof(MainActivity));
            //Use Notification Builder 
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this);
            //Create the notification 
            //we use the pending intent, passing our ui intent over which will get called 
            //when the notification is tapped. 



            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InSampleSize = 2;
           
           

            
            WebClient client = new WebClient();
            System.IO.Stream stream = client.OpenRead(image);
            Bitmap myBitmap = BitmapFactory.DecodeStream(stream,null,options);

            var notification = builder.SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, 0))
                .SetSmallIcon(Android.Resource.Drawable.SymActionEmail)
                .SetTicker(title)
                .SetContentTitle(title)
                .SetContentText(desc)
                .SetStyle(new NotificationCompat.BigPictureStyle().BigPicture(myBitmap))

                //Set the notification sound
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                //Auto cancel will remove the notification once the user touches it 
                .SetAutoCancel(true).Build();
            //Show the notification 
            notificationManager.Notify(1, notification);
           


        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "Unregistered RegisterationId : " + registrationId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "GCM Error: " + errorId);
        }
    }
}