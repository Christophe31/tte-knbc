﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.BusinessLayer
{
	public partial class SubjectData : object, System.Runtime.Serialization.IExtensibleDataObject
	{
		public SubjectData()
		{

		}

        public static string[] PresetModalities
        {
            get
            {
                return new string[] { "Cours", "E-learning", "TD", "Examen", "TP", "Soutenance" };
            }
        }
	}
}
