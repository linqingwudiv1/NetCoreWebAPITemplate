﻿using Microsoft.Extensions.Configuration;
using System;

namespace BaseDLL
{
    /// <summary>
    /// 
    /// </summary>
    public static class GVariable
    {

        /// <summary>
        /// 
        /// </summary>
        public static IConfiguration configuration;


        /// <summary>
        /// 默认删除时间
        /// </summary>
        public static readonly DateTime DefDeleteTime = new DateTime(1900, 1, 1);


    }
}
