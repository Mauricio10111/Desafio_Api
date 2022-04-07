using System;

namespace Desafio_Api.Models
{
    public class VeryBigSumModel
    {
        public Guid Id { get; set; } = new Guid();
        public string Input {get; set;}
        public string Output { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}