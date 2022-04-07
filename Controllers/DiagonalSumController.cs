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
    public class DiagonnalSumController : ControllerBase
    {
        private int _sum;
        private int _dataTypeToCompare;
        private readonly AppDbContext _context;



        public DiagonnalSumController(AppDbContext context)
        {
            _context = context;
        }

        //Get: api/VeryBigSum/{id}
        //return element by id
        [HttpGet("api/[controller]/GetValueById/{id}")]
        public async Task<ActionResult<DiagonalSumModel>> GetValueById(Guid id)
        {
            var value = await _context.DiagonalSumModel.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            return Ok(value);
        }



        //Post: api/VeryBigSum
        // create a new element
        [HttpPost]
        [Route("api/[controller]/PostValue")]
        public ActionResult PostValue([FromBody] DiagonalSumEntryModel entryModel)
        {
            DiagonalSumModel dados = new DiagonalSumModel();
            dados.Id = new Guid();


            int sumLeftToRight = entryModel.arr1[0] + entryModel.arr2[1] + entryModel.arr3[2];
            int sumRightToLeft = entryModel.arr1[2] + entryModel.arr2[1] + entryModel.arr3[0];
            int result = Math.Abs(sumLeftToRight - sumRightToLeft);

            List<int> list = new List<int>();
            list.AddRange(entryModel.arr1);
            list.AddRange(entryModel.arr2);
            list.AddRange(entryModel.arr3);
            int[] arrayInput = list.ToArray();


            var resultadoInput = String.Join(",", arrayInput);

            dados.Input = resultadoInput;
            dados.Output = result.ToString();
            _context.DiagonalSumModel.Add(dados);
            _context.SaveChangesAsync();


            return Ok(result);

        }


        //Delete:api/VeryBigSum/{id}
        //Remove element by id
        [HttpDelete("api/[controller]/DeleteValueById/{id}")]
        public async Task<IActionResult> DeleteValueById(Guid id)
        {
            var value = await _context.DiagonalSumModel.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }
            _context.DiagonalSumModel.Remove(value);
            await _context.SaveChangesAsync();
            //return NoContent();
            return Ok("Foi apagado");
        }



        // Get: api/VeryBigSum
        //Return all values of DiagonalSumModel
        [HttpGet("api/[controller]/GetAllValues")]

        public ActionResult<IEnumerable<DiagonalSumModel>> GetAllValues()
        {
            var ListOfVeryBigSumModels = _context.DiagonalSumModel.OrderByDescending(x => x.Date).ToList();
            return Ok(ListOfVeryBigSumModels);
        }
    }
}