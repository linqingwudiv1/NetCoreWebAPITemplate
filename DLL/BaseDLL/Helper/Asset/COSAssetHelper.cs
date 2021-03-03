using COSSTS;
using COSXML;
using COSXML.Auth;
using COSXML.Model.Object;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using System.Threading.Tasks;
using TencentCloud.Common;
using TencentCloud.Common.Profile;
using TencentCloud.Sts.V20180813;
using TencentCloud.Sts.V20180813.Models;

namespace BaseDLL.Helper.Asset
{
    /// <summary>
    /// 
    /// </summary>
    public class COSAssetHelper : IAssetHelper
    {
        /// <summary>
        /// 
        /// </summary>
        readonly CosXmlServer cosXml;

        /// <summary>
        /// 
        /// </summary>
        readonly string secretId;
        
        /// <summary>
        /// 
        /// </summary>
        readonly string secretKey;

        /// <summary>
        /// 
        /// </summary>
        readonly string region;

        /// <summary>
        /// 
        /// </summary>
        readonly string appID;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_secretId"></param>
        /// <param name="_secretKey"></param>
        public COSAssetHelper(string _appID, string _secretId, string _secretKey, string _region) 
        {
            //初始化 CosXmlConfig 
            // string appid = "1250000000";//设置腾讯云账户的账户标识 APPID
            this.region = _region;  //设置一个默认的存储桶地域

            this.appID = _appID;

            CosXmlConfig config = new CosXmlConfig.Builder()
              .IsHttps(true)  //设置默认 HTTPS 请求
              .SetRegion(region)  //设置一个默认的存储桶地域
              .SetDebugLog(true)  //显示日志
              .SetAppid(this.appID)
              .Build();  //创建 CosXmlConfig 对象

            this.secretId = _secretId; //"云 API 密钥 SecretId";
            this.secretKey = _secretKey; //"云 API 密钥 SecretKey";
            long durationSecond = 600;  //每次请求签名有效时长，单位为秒
            QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(secretId, secretKey, durationSecond);

            this.cosXml = new CosXmlServer(config, cosCredentialProvider);

        }

        public bool Add(string bucket, string key, byte[] data)
        {
            try
            {
                PutObjectRequest request = new PutObjectRequest(bucket, key, data);

                //执行请求
                PutObjectResult result = cosXml.PutObject(request);
                //对象的 eTag
                string eTag = result.eTag;

                return true;
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
                Debug.WriteLine("CosClientException: " + clientEx);
                return false;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Debug.WriteLine("CosServerException: " + serverEx.GetInfo());
                return false;
            }

        }

        public bool Add(string bucket, string key, string assetPath)
        {
            try
            {
                PutObjectRequest request = new PutObjectRequest(bucket, key, assetPath);
                //设置进度回调
                //执行请求
                PutObjectResult result = cosXml.PutObject(request);
                //对象的 eTag
                string eTag = result.eTag;
                return true;
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
                Debug.WriteLine("CosClientException: " + clientEx);
                return false;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Debug.WriteLine("CosServerException: " + serverEx.GetInfo());
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string ConvertToKey(string url)
        {
            try
            {

                var uri = new Uri(url);
                return uri.AbsolutePath;
            }
            catch (Exception) 
            {
                return "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete(string bucket, string key)
        {
            try
            {
                DeleteObjectRequest request = new DeleteObjectRequest(bucket, key);
                //执行请求
                DeleteObjectResult result = cosXml.DeleteObject(request);
                return true;
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
                Debug.WriteLine("CosClientException: " + clientEx);
                return false;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                Debug.WriteLine("CosServerException: " + serverEx.GetInfo());
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte[] Get(string bucket, string key)
        {
            try
            {
                GetObjectBytesRequest request = new GetObjectBytesRequest(bucket, key);
                //设置进度回调
                //执行请求
                GetObjectBytesResult result = cosXml.GetObject(request);
                //获取内容
                byte[] content = result.content;
                //请求成功
                return content;
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException:======================= " + clientEx);
                Debug.WriteLine("CosClientException:======================= " + clientEx);
                return new byte[] { };
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException:======================= " + serverEx.GetInfo());
                Debug.WriteLine("CosServerException:======================= " + serverEx.GetInfo());
                return new byte[] { };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="allowPrefixs"></param>
        /// <param name="allowActions"></param>
        /// <param name="keepTime"></param>
        /// <returns></returns>
        public dynamic GetTempToken(string bucket, string[] allowPrefixs = null, string[] allowActions = null, int keepTime = 1800)
        {
            // 默认运行客户端上传到云存储服务器
            allowActions = allowActions ?? new string[] 
            {
                "name/cos:PutObject",
                "name/cos:PostObject",
                "name/cos:InitiateMultipartUpload",
                "name/cos:ListMultipartUploads",
                "name/cos:ListParts",
                "name/cos:UploadPart",
                "name/cos:CompleteMultipartUpload"
            };

            allowPrefixs = allowPrefixs ?? new string[] { "*" };

            Dictionary<string, object> dire = new Dictionary<string, object>();

            dire.Add("bucket",          bucket);
            dire.Add("region",          this.region);
            dire.Add("allowPrefix",     allowPrefixs[0]);
            dire.Add("allowPrefixs",    allowPrefixs);
            dire.Add("allowActions",    allowActions );
            dire.Add("durationSeconds", keepTime);

            dire.Add("secretId",  this.secretId);
            dire.Add("secretKey", this.secretKey);

            dire.Add("Domain", "sts.tencentcloudapi.com");


            var credential = STSClient.genCredential(dire);
            return credential;
            //throw new NotImplementedException();
        }

        public bool hasKey(string bucket, string key)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Update(string bucket, string key, string assetPath)
        {
            try
            {
                PutObjectRequest request = new PutObjectRequest(bucket, key, assetPath);
                // 执行请求
                PutObjectResult result = cosXml.PutObject(request);
                // 对象的 eTag
                string eTag = result.eTag;
                //
                return true;
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
                return false;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                return false;
            }
        }

        public bool Update(string bucket, string key, byte[] data)
        {
            try
            {
                PutObjectRequest request = new PutObjectRequest(bucket, key, data);
                //执行请求
                PutObjectResult result = cosXml.PutObject(request);
                //对象的 eTag
                string eTag = result.eTag;

                return true;
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
                return false;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                return false;
            }
        }
    }
}
