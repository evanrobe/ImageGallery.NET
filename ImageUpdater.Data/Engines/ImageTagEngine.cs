using ImageUpdater.Foundation.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageUpdater.Foundation.Messages;
using System.Data.SqlClient;
using System.Data.SqlServerCe;

namespace ImageUpdater.Data.Engines
{
    public class ImageTagEngine : IImageTagEngine
    {
        public void Insert(ImageTag i)
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["imageDB"].ConnectionString;
            var con = new SqlCeConnection(cs);
            var sql = @"insert 
                       into 
                       IMAGE_TAG 
                       (
                       I_ID ,
                       IT_NAME
                       )
                       VALUES
                       (
                       @iId ,
                       @itName
                       )
                       ";

            con.Open();

            var command = new SqlCeCommand(sql, con);

            command.Parameters.AddWithValue("@iId", i.ImageId);
            command.Parameters.AddWithValue("@itName", i.TagName);
            command.ExecuteNonQuery();

            sql = "select @@IDENTITY as id";
            command = new SqlCeCommand(sql, con);

            var reader = command.ExecuteReader();

            while(reader.Read())
            {
                i.ID = Decimal.ToInt32((Decimal)reader["id"]);
            }
        }
        public IEnumerable<ImageTag> RetrieveByImageId(int imageId)
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["imageDB"].ConnectionString;
            var con = new SqlCeConnection(cs);
            var sql = "";
            var ret = new List<ImageTag>();

            con.Open();

            sql = @"select 
                    I.I_ID,
                    I_GUID,
                    I_FILE_NAME,
                    I_CONTENT_TYPE,
                    IT_ID,
                    IT_NAME
                    FROM
                    IMAGE I
                    JOIN
                    IMAGE_TAG IT
                    ON
                    IT.I_ID = I.I_ID
                    WHERE
                    I.I_ID = @iId";

            var command = new SqlCeCommand(sql, con);

            command.Parameters.AddWithValue("@iId", imageId);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ImageTag i = new ImageTag();

                i.ID = (int)reader["IT_ID"];
                i.TagName = (String)reader["IT_NAME"];
                i.roImageGUID = Guid.Parse((String)reader["I_GUID"]);
                i.roFileName = (String)reader["I_FILE_NAME"];
                i.roContentType = (String)reader["I_CONTENT_TYPE"];
                ret.Add(i);
            }

            return ret;
        }

        public void Delete(int Id)
        {
            var cs = System.Configuration.ConfigurationManager.ConnectionStrings["imageDB"].ConnectionString;
            var con = new SqlCeConnection(cs);
            var sql = @"delete
                       FROM
                       IMAGE_TAG 
                       WHERE
                       IT_ID = @itId
                       ";

            con.Open();

            var command = new SqlCeCommand(sql, con);

            command.Parameters.AddWithValue("@itId", Id);
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {

        }

        
    }
}
