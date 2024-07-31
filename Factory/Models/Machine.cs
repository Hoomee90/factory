using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
	public class Machine
	{
		public int MachineId { get; set; }
		[Required(ErrorMessage = "The machine must have a name")]
		public string Name { get; set; }
		[Required(ErrorMessage = "The machine must have a level of danger")]
		public string DangerLevel { get; set; }
		public List<EngineerMachine> JoinEntities { get; }
	}
}