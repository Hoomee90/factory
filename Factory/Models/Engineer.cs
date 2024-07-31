using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
	public class Engineer
	{
		public int EngineerId { get; set; }
		[Required(ErrorMessage = "The engineer must have a name")]
		public string Name { get; set; }
		[Required(ErrorMessage = "The engineer must have an attitude")]
		public string Attitude { get; set; }
		[Range(1, int.MaxValue, ErrorMessage = "The engineer must have a number of years worked")]
		public int YearsWorked { get; set; }
		public List<EngineerMachine> JoinEntities { get; }
	}
}