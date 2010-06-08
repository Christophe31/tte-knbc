using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.BusinessService
{
	public partial class SubjectData : IdName, System.Runtime.Serialization.IExtensibleDataObject
	{
		public SubjectData()
		{

		}

        public static string[] PresetModalities
        {
            get
            {
                return new string[] { "Cours", "E-learning", "TD", "TP", "Soutenance", "Examen" };
            }
        }
	}
}
