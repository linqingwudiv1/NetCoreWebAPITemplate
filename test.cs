		class Test
        {
        
        public void LoadAppSettings()
		{
			try
			{
				ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
				fileMap.ExeConfigFilename = "";
				System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
				foreach (KeyValueConfigurationElement element in configuration.AppSettings.Settings)
				{
					ConfigurationSettings.AppSettings[element.Key] = element.Value;
				}
			}
			catch (Exception exception)
			{
				throw new Exception("config文件不存在或配置错误." + exception.Message);
			}
		}


        }
