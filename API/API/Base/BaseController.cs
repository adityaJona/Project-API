using API.Models;
using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        // Mengambil semua data
        [HttpGet]
        public ActionResult<Entity> Get()
        {
            var result = repository.Get();
            //try
            //{
            //    if (result.Count() > 0)
            //    {
            //        return Ok(new { status = HttpStatusCode.OK, Result = result, Message = "Data Ditemukan" });
            //    }
            //    else
            //    {
            //        return NotFound(new { status = HttpStatusCode.NotFound, Result = result, Message = "Data Tidak Ditemukan" });
            //    }
            //}
            //catch
            //{
            //    return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result, message = "Ada Kesalahan" });
            //}
            return Ok(result);
        }

        // Mengambil Data Berdasarkan PK
        [HttpGet("{key}")]
        public ActionResult<Entity> GetByPK(Key key)
        {
            var result = repository.Get(key);
            try
            {
                if(result == null)
                {
                    return NotFound(new { Status = HttpStatusCode.NotFound, Result = result, Message = "Data Tidak Ditemukan" });
                }
                else
                {
                    return Ok(new { Status = HttpStatusCode.OK, Result = result, Message = "Data Ditemukan" });
                }
            }
            catch
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result, message = "Ada Kesalahan" });
            }
        }

        [HttpPost]
        public ActionResult<Entity> Create(Entity entity)
        {
            try
            {
                var result = repository.Insert(entity);
                return Ok(new { status = HttpStatusCode.OK, Result = result, Message = "Data Berhasil Ditambahkan" });
            }
            catch
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Ada Kesalahan" });
            }
        }

        [HttpDelete("{key}")]
        public ActionResult<Entity> DeleteByPK(Key key)
        {
            try
            {
                if (key == null)
                {
                    return NotFound(new { status = HttpStatusCode.NotFound, Message = "Data Gagal Dihapus" });
                }
                else
                {
                    var result = repository.Delete(key);
                    return Ok(new { status = HttpStatusCode.OK, Result = result, Message = "Data Berhasil Dihapus" });
                }
            }
            catch
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Ada Kesalahan" });
            }
        }

        [HttpPut]
        public ActionResult<Entity> Update(Entity entity, Key key)
        {
            try
            {
                var result = repository.Update(entity,key);
                return Ok(new { status = HttpStatusCode.OK, Result = result, Message = "Data Berhasil Diupdate" });
            }
            catch
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Ada Kesalahan" });
            }
        }
    }
}
