using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Desafio_Api.Data;
using Desafio_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Api.Controllers
{
    public class VeryBigSumController : ControllerBase
    {

        private int _dataTypeToCompare;
        private readonly AppDbContext _context;

        public VeryBigSumController(AppDbContext context)
        {
            _context = context;
        }

        //Get: api/VeryBigSum/{id}
        //return element by id
        [HttpGet("/api/[controller]/GetValueById/{id}")]
        public async Task<ActionResult<VeryBigSumModel>> GetValueById(Guid id)
        {
            var value = await _context.VeryBigSumModels.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }




        //Post: api/VeryBigSum
        // create a new element
        [HttpPost]
        [Route("/api/[controller]/PostValue")]
        public ActionResult PostValue([FromBody] int[] arr)
        {

            if ((arr.GetType().GetElementType() == _dataTypeToCompare.GetType()) && (arr.Length == 5))
            {
                VeryBigSumModel obj = new VeryBigSumModel();
                obj.Id = new Guid();
                int _sum = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    _sum += arr[i];

                }
                obj.Input = string.Join(",", arr);
                obj.Output = _sum.ToString();


                _context.VeryBigSumModels.Add(obj);
                _context.SaveChangesAsync();

                return Ok(obj);
            }
            return null;

        }


        //Delete:api/VeryBigSum/{id}
        //Remove element by id
        [HttpDelete("/api/[controller]/DeleteValueById/{id}")]
        public async Task<IActionResult> DeleteValueById(Guid id)
        {
            var value = await _context.VeryBigSumModels.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            _context.VeryBigSumModels.Remove(value);
            await _context.SaveChangesAsync();
            //return NoContent();
            return Ok("Foi apagado");
        }



        // Get: api/VeryBigSum
        //Return all values of VeryBigSumModels
        [HttpGet("/api/[controller]/GetAllValues")]

        public ActionResult<IEnumerable<VeryBigSumModel>> GetAllValues()
        {
          

            var ListOfVeryBigSumModels = _context.VeryBigSumModels.OrderByDescending(x => x.Date).ToList();
            return Ok(ListOfVeryBigSumModels);
            

        }



    }
}