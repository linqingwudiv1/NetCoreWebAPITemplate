using BaseDLL;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NetApplictionServiceDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebCoreService.Controllers.Asset
{

    class Signature
    {
        /// <summary>
        /// 
        /// </summary>
        public string m_strSecId;
        /// <summary>
        /// 
        /// </summary>
        public string m_strSecKey;
        /// <summary>
        /// 
        /// </summary>
        public int m_iRandom;
        /// <summary>
        /// 
        /// </summary>
        public long m_qwNowTime;
        /// <summary>
        /// 
        /// </summary>
        public int m_iSignValidDuration;
        public static long GetIntTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1);
            return Convert.ToInt64(ts.TotalSeconds);
        }
        private byte[] hash_hmac_byte(string signatureString, string secretKey)
        {
            var enc = Encoding.UTF8; HMACSHA1 hmac = new HMACSHA1(enc.GetBytes(secretKey));
            hmac.Initialize();
            byte[] buffer = enc.GetBytes(signatureString);
            return hmac.ComputeHash(buffer);
        }
        public string GetUploadSignature()
        {
            string strContent = "";
            strContent += ("secretId=" + Uri.EscapeDataString((m_strSecId)));
            strContent += ("&currentTimeStamp=" + m_qwNowTime);
            strContent += ("&expireTime=" + (m_qwNowTime + m_iSignValidDuration));
            strContent += ("&random=" + m_iRandom);
            byte[] bytesSign = hash_hmac_byte(strContent, m_strSecKey);
            byte[] byteContent = System.Text.Encoding.Default.GetBytes(strContent);
            byte[] nCon = new byte[bytesSign.Length + byteContent.Length];
            bytesSign.CopyTo(nCon, 0);
            byteContent.CopyTo(nCon, bytesSign.Length);
            return Convert.ToBase64String(nCon);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Route("api/asset/[controller]")]
    [EnableCors("WebAPIPolicy")]
    [ApiController]
    public class VedioController : BaseController
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetVodSignature")]
        public async Task<ActionResult> GetVodSignature()
        {
            try
            {
                Signature sign = new Signature();
                sign.m_strSecId = GVariable.configuration["VOD:SecretId"];
                sign.m_strSecKey = GVariable.configuration["VOD:SecretKey"];
                sign.m_qwNowTime = Signature.GetIntTimeStamp();
                sign.m_iRandom = new Random().Next(0, 1000000);
                sign.m_iSignValidDuration = 3600 * 24 * 2;

                return JsonToCamelCase(new 
                {
                    signature = sign.GetUploadSignature(),
                    timestamp = sign.m_qwNowTime,
                    validDuration = sign.m_iSignValidDuration
                });
            }
            catch (Exception ex) 
            {
                return JsonToCamelCase(ex.Message, 50000, 50000, ex.Message);
            }
        }
    }
}
