using Airbnb.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace Airbnb.Database.Singleton
{
	/// <summary>
	/// Třída pro vytvoření podle návrhového vzoru Singleton
	/// </summary>
   public class DatabaseSingleton
    {
		/// <summary>
		///textový řetezec, který obsahuje informace k připojení se k DB 
		/// </summary>
			public static string connString = "Data Source=;Initial Catalog=Airbnb;User ID=;Password=";
			private static SqlConnection conn = null;
			private DatabaseSingleton()
			{
			}

		/// <summary>
		/// Instance třídy, pomocí metody ReadSettings() získávám potřebné informace z App.config souboru
		/// </summary>
		/// <returns>Vrací SqlConnection object</returns>
		public static SqlConnection GetInstance()
		{

            try
            {
				if (conn == null)
				{
					SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();
					consStringBuilder.UserID = ReadSetting("Name");
                    if (consStringBuilder.Equals("sa"))
                    {
						throw new InvalidInput("sa user is not allowes");
                    }
					consStringBuilder.Password = ReadSetting("Password");
					consStringBuilder.InitialCatalog = ReadSetting("Database");
					consStringBuilder.DataSource = ReadSetting("DataSource");
					consStringBuilder.MultipleActiveResultSets = true;
					consStringBuilder.ConnectTimeout = 30;
					conn = new SqlConnection(consStringBuilder.ConnectionString);
					conn.Open();

				}
			}
            catch (InvalidInput ex)
            {

				MessageBox.Show(ex.Message);
            }
				
			
			
			return conn;
		}
		/// <summary>
		/// Zavírá připojení k dané DB
		/// </summary>
		public static void CloseConnection()
			{
				if (conn != null)
				{
					conn.Close();
					conn.Dispose();
				}
			}
		/// <summary>
		/// Metoda, která přečte a vrátí informace z App.config souboru
		/// </summary>
		/// <param name="key">klíč(hodnota z conf. souboru)</param>
		/// <returns>Vrátí klíč v textové podobě</returns>
			private static string ReadSetting(string key)
			{
				
				var appSettings = ConfigurationManager.AppSettings;
				string result = appSettings[key] ?? "Not Found";
				return result;
			}

		public static bool IsConnected(SqlConnection conn)
		{
			bool isConnected = false;
            try
            {
				if (conn == null)
				{
					throw new ConnectionException("It is not possible to connect to the database. Please check your settings and try again");

                }
                else
                {
					isConnected = true;
                }
			}
            catch (ConnectionException ex)
            {

				MessageBox.Show(ex.Message);
            }
			return isConnected;
		}
	}

	}

