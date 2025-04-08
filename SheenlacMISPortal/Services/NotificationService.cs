using CorePush.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using SheenlacMISPortal.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static SheenlacMISPortal.Models.GoogleNotification;
namespace SheenlacMISPortal.Services
{
   
        public interface INotificationService
        {
            Task<ResponseModel> SendNotification(NotificationModel notificationModel);
        }

    public class NotificationService : INotificationService
    {
        private readonly FcmNotificationSetting _fcmNotificationSetting;
        public NotificationService(IOptions<FcmNotificationSetting> settings)
        {
            _fcmNotificationSetting = settings.Value;
        }
    
            public async Task<ResponseModel> SendNotification(NotificationModel notificationModel)
            {
                ResponseModel response = new ResponseModel();
                try
                {
                    if (notificationModel.IsAndroiodDevice)
                    {

                  

                    string     Username = "sureshbv@sheenlac.in";
                      string   Password = "admin123";



                        Token token = new Token();
                        HttpClientHandler handler = new HttpClientHandler();
                        HttpClient client = new HttpClient(handler);
                        var RequestBody = new Dictionary<string, string>
                {
                {"username", Username},
                {"password", Password},
                };
                     string baseAddress = "http://13.234.246.143/api/v2/auth";

        var tokenResponse = client.PostAsync(baseAddress, new FormUrlEncodedContent(RequestBody)).Result;

                        if (tokenResponse.IsSuccessStatusCode)
                        {
                            var JsonContent = tokenResponse.Content.ReadAsStringAsync().Result;

                            JObject studentObj = JObject.Parse(JsonContent);

                            var result = JObject.Parse(JsonContent);   //parses entire stream into JObject, from which you can use to query the bits you need.
                            var items = result["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in the form of JTokens)

                            token.access_token = (string)items[0];
                            token.Error = null;
                        }
                        else
                        {
                            token.Error = "Not able to generate Access Token Invalid usrename or password";
                        }
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

                        string myjson1 = "";

                        //  string jsonarray = "" + trnid + ":" + op2 + "";
                        //string strFormat="{"transactionId":" + op2 + "}"


                        //  var data2 = new System.Net.Http.StringContent(myjson, Encoding.UTF8, "application/json");
                        //var json = Newtonsoft.Json.JsonConvert.SerializeObject(data2);
                        var data1 = new System.Net.Http.StringContent(myjson1, Encoding.UTF8, "application/json");            // dev
                        var url = "http://13.233.6.115/api/v2/billException/gettokendata";
                        var response3 = await client.PostAsync(url, data1);
                        string result7 = response3.Content.ReadAsStringAsync().Result;




                        //    JObject studentObj = JObject.Parse(result7);

                        var result1 = JObject.Parse(result7);   //parses entire stream into JObject, from which you can use to query the bits you need.
                        var items1 = result1["data"].Children().ToList();



                        //  var items1 = result7["data"].Children().ToList();   //Get the sections you need and save as enumerable (will be in 
                        var jsonString2 = Newtonsoft.Json.JsonConvert.SerializeObject(items1[0]);
                        //   var RootPayment1 = JsonConvert.DeserializeObject<List<Models.Painterbank_details>>(jsonString2);
                        jsonString2 = jsonString2.Replace("\"access\":", "");
                        //"access":
                        // dynamic data = JsonConvert.DeserializeObject<dynamic>(jsonString2.ToString());

                    

                    /* FCM Sender (Android Device) */
                    FcmSettings settings = new FcmSettings()
                        {
                            SenderId = _fcmNotificationSetting.SenderId,
                            ServerKey = _fcmNotificationSetting.ServerKey
                        };
                        HttpClient httpClient = new HttpClient();
                    settings.ServerKey = "";
                    settings.ServerKey = jsonString2;


                        string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                        string deviceToken = notificationModel.DeviceId;

                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                        httpClient.DefaultRequestHeaders.Accept
                                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        DataPayload dataPayload = new DataPayload();
                        dataPayload.Title = notificationModel.Title;
                        dataPayload.Body = notificationModel.Body;

                        GoogleNotification notification = new GoogleNotification();
                        notification.Data = dataPayload;
                        notification.Notification = dataPayload;

                        var fcm = new FcmSender(settings, httpClient);
                        var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    Exceptionlog.Logexception(settings.ServerKey, "notifylog");
                    if (fcmSendResponse.IsSuccess())
                        {
                            response.IsSuccess = true;
                            response.Message = "Notification sent successfully";
                            return response;
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = fcmSendResponse.Results[0].Error;
                            return response;
                        }
                    }
                    else
                    {
                        /* Code here for APN Sender (iOS Device) */
                        //var apn = new ApnSender(apnSettings, httpClient);
                        //await apn.SendAsync(notification, deviceToken);
                    }
                    return response;
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Something went wrong";
                    return response;
                }
            }
        }
}
