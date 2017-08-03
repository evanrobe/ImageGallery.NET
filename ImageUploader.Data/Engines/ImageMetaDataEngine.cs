using ImageUploader.Foundation.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageUploader.Foundation.Messages;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace ImageUploader.Data.Engines
{
    public class ImageMetaDataEngine : IImageMetaDataEngine
    {
        public void Insert(ImageMetaData i)
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["imageDB"].ConnectionString;
            var con = new SqlCeConnection(cs);
            var sql = "insert into IMAGE(I_GUID , I_FILE_NAME, I_CONTENT_TYPE) values (@iGuid , @iFileName , @iContentType)";

            con.Open();

            var command = new SqlCeCommand(sql, con);

            command.Parameters.AddWithValue("@iGuid", i.ImageGUID.ToString());
            command.Parameters.AddWithValue("@iFileName", i.FileName);
            command.Parameters.AddWithValue("@iContentType", i.ContentType);
            command.ExecuteNonQuery();

            sql = "select @@IDENTITY as id";
            command = new SqlCeCommand(sql, con);

            var reader = command.ExecuteReader();

            while(reader.Read())
            {
                i.ID = Decimal.ToInt32((Decimal)reader["id"]);
            }
        }
        public IEnumerable<ImageMetaData> RetrieveAll()
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["imageDB"].ConnectionString;
            var con = new SqlCeConnection(cs);
            var sql = "";
            var ret = new List<ImageMetaData>();

            con.Open();

            sql = @"select 
                    I_ID,
                    I_GUID,
                    I_FILE_NAME,
                    I_CONTENT_TYPE
                    FROM
                    IMAGE";

            var command = new SqlCeCommand(sql, con);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ImageMetaData i = new ImageMetaData();

                i.ID = (int)reader["I_ID"];
                i.ImageGUID = Guid.Parse((String)reader["I_GUID"]);
                i.FileName = (String)reader["I_FILE_NAME"];
                i.ContentType = (String)reader["I_CONTENT_TYPE"];
                ret.Add(i);
            }

            return ret;
        }

        public IEnumerable<ImageMetaData> RetrieveByPartialTagName(String tagName)
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["imageDB"].ConnectionString;
            var con = new SqlCeConnection(cs);
            var sql = "";
            var ret = new List<ImageMetaData>();

            con.Open();

            sql = @"select 
                    distinct
                    I.I_ID,
                    I.I_GUID,
                    I.I_FILE_NAME,
                    I.I_CONTENT_TYPE
                    FROM
                    IMAGE I
                    JOIN
                    IMAGE_TAG IT
                    ON
                    IT.I_ID = I.I_ID
                    WHERE
                    IT.IT_NAME LIKE @tagName
                    ";

            var command = new SqlCeCommand(sql, con);

            command.Parameters.AddWithValue("@tagName" , "%" + tagName + "%");

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ImageMetaData i = new ImageMetaData();

                i.ID = (int)reader["I_ID"];
                i.ImageGUID = Guid.Parse((String)reader["I_GUID"]);
                i.FileName = (String)reader["I_FILE_NAME"];
                i.ContentType = (String)reader["I_CONTENT_TYPE"];
                ret.Add(i);
            }

            return ret;
        }

        public ImageMetaData RetrieveByGuid(Guid imageGuid)
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["imageDB"].ConnectionString;
            var con = new SqlCeConnection(cs);
            var sql = "";
            ImageMetaData ret = null;

            con.Open();

            sql = @"select 
                    I_ID,
                    I_GUID,
                    I_FILE_NAME,
                    I_CONTENT_TYPE
                    FROM
                    IMAGE
                    WHERE
                    I_GUID = @iGuid";

            var command = new SqlCeCommand(sql, con);

            command.Parameters.AddWithValue("@iGuid", imageGuid.ToString());
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ret = new ImageMetaData();

                ret.ID = (int)reader["I_ID"];
                ret.ImageGUID = Guid.Parse((String)reader["I_GUID"]);
                ret.FileName = (String)reader["I_FILE_NAME"];
                ret.ContentType = (String)reader["I_CONTENT_TYPE"];
            }

            return ret;
        }

        public ImageMetaData RetrieveById(int Id)
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["imageDB"].ConnectionString;
            var con = new SqlCeConnection(cs);
            var sql = "";
            ImageMetaData ret = null;

            con.Open();

            sql = @"select 
                    I_ID,
                    I_GUID,
                    I_FILE_NAME,
                    I_CONTENT_TYPE
                    FROM
                    IMAGE
                    WHERE
                    I_ID = @iId";

            var command = new SqlCeCommand(sql, con);

            command.Parameters.AddWithValue("@iId", Id);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ret = new ImageMetaData();

                ret.ID = (int)reader["I_ID"];
                ret.ImageGUID = Guid.Parse((String)reader["I_GUID"]);
                ret.FileName = (String)reader["I_FILE_NAME"];
                ret.ContentType = (String)reader["I_CONTENT_TYPE"];
            }

            return ret;
        }

        public void Update(ImageMetaData i)
        {
            throw new NotImplementedException();
        }
        public void Delete(int imageId)
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["imageDB"].ConnectionString;
            var con = new SqlCeConnection(cs);
            var sql = @"DELETE
                       FROM
                       IMAGE
                       WHERE
                       I_ID = @iId";

            con.Open();

            var command = new SqlCeCommand(sql, con);

            command.Parameters.AddWithValue("@iId", imageId);
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {

        }

        
    }
}
