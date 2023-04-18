using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismo.Models;
using AndreTurismo.Services;

namespace AndreTurismo.Controllers
{
    public class PackageController
    {
        public bool Insert(Package pack)
        {
            return new PackageServices().InsertPack(pack);
        }
    }
}
