using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Model;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrariController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public OrariController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select DitaJaves, Departmenti, Lenda, LendaId, Profesori, OrariM, 
                FormatiM, Salla,
                Grupi
                from dbo.Orari
                ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProjektiConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);

        }

        [HttpPost]
        public JsonResult Post(Orari or)
        {
            string query = @"
                        insert into dbo.Orari 
                    (DitaJaves,Departmenti,Lenda,LendaId,Profesori,OrariM,FormatiM,Salla,Grupi)
                    values
                    (
                    '" + or.DitaJaves + @"'
                    ,'" + or.Departmenti + @"'
                    ,'" + or.Lenda + @"'
                    ,'" + or.LendaId + @"'
                    ,'" + or.Profesori + @"'
                    ,'" + or.OrariM + @"'
                    ,'" + or.FormatiM + @"'
                    , '" + or.Salla + @"'
                    ,'" + or.Grupi + @"'
                     )
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProjektiConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("U shtua me sukses");

        }

        [HttpPut]
        public JsonResult Put(Orari or)
        {
            string query = @"
                        update dbo.orari set 
                DitaJaves ='" + or.DitaJaves + @"'
                ,Departmenti ='" + or.Departmenti + @"'
                ,Lenda ='" + or.Lenda + @"'
                ,LendaId ='" + or.LendaId + @"'
                ,Profesori ='" + or.Profesori + @"'
                ,OrariM ='" + or.OrariM + @"'
                ,FormatiM ='" + or.FormatiM + @"'
                ,Salla ='" + or.Salla + @"'
                ,Grupi ='" + or.Grupi + @"'
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProjektiConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("U perditesua me sukses");

        }


            
        [HttpDelete("{id}")]
        public JsonResult Delete(int id )
        {
            string query = @"
                        delete from dbo.Orari 
                where LendaId = " + id + @"
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProjektiConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("U fshi me sukses");
        }



    }
}
