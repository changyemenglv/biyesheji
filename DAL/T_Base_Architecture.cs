﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class T_Base_Architecture
    {
        /// <summary>
        /// 连接到数据库
        /// </summary>
        /// <returns></returns>
        private SqlConnection SqlServerOpen()
        {
            SqlConnection co = new SqlConnection();
            co.ConnectionString = "server=212.64.18.220;uid=bysj;pwd=bysj;database=bysj";
            co.Open();
            return co;
        }

        /// <summary>
        /// 获取全部建筑
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageNumber"></param>
        /// <returns></returns>
        public List<Model.T_Base_Architecture> GetAllArchitecture()
        {
            List<Model.T_Base_Architecture> list = new List<Model.T_Base_Architecture>();
            SqlConnection co = SqlServerOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select * from T_Base_Architecture";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Model.T_Base_Architecture architecture = new Model.T_Base_Architecture();
                architecture.Id = Convert.ToInt32(reader["Id"]);
                architecture.ArchitectureName = Convert.ToString(reader["ArchitectureName"]);
                architecture.IsCollege = Convert.ToInt32(reader["IsCollege"]);
                list.Add(architecture);
            }
            reader.Close();
            co.Close();
            return list;
        }


        /// <summary>
        /// 保存新增地点信息
        /// </summary>
        /// <param name="Architecture"></param>
        /// <returns></returns>
        public int AddSaveArchitecture(Model.T_Base_Architecture Architecture)
        {
            SqlConnection co = SqlServerOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "insert into T_Base_Architecture values('" 
                + Architecture.ArchitectureName + "',"+Architecture.IsCollege+")";
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        /// <summary>
        /// 根据Id获取指定建筑信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Model.T_Base_Architecture GetArchitecture(int Id)
        {
            SqlConnection co = SqlServerOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "select * from T_Base_Architecture where Id = "+Id;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Model.T_Base_Architecture architecture = new Model.T_Base_Architecture();
            architecture.Id = Convert.ToInt32(reader["Id"]);
            architecture.ArchitectureName = Convert.ToString(reader["ArchitectureName"]);
            architecture.IsCollege = Convert.ToInt32(reader["IsCollege"]);
            return architecture;
        }

        /// <summary>
        /// 保存修改后建筑的信息
        /// </summary>
        /// <param name="Architecture"></param>
        /// <returns></returns>
        public int EditSaveArchitecture(Model.T_Base_Architecture Architecture)
        {
            SqlConnection co = SqlServerOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "update T_Base_Architecture set ArchitectureName = '" 
                + Architecture.ArchitectureName+"',IsCollege = "+Architecture.IsCollege+
                " where Id = "+Architecture.Id;
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        /// <summary>
        /// 删除场地
        /// </summary>
        /// <param name="Ids"></param>
        /// <returns></returns>
        public int Delete(string Ids)
        {
            SqlConnection co = SqlServerOpen();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = co;
            cmd.CommandText = "delete from T_Base_Place where ArchitectureId in (" + Ids + ")";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "delete from T_Base_Architecture where id in (" + Ids + ")";
            int result = cmd.ExecuteNonQuery();
            co.Close();
            return result;
        }

    }
}
