using System;
using OnlineTrgovina.Mappers;
using OnlineTrgovina.Models.DTO;
using OnlineTrgovina.Models;
using OnlineTrgovina.Models.DTO;

namespace EdunovaAPI.Mappings
{
    public static class Mapping
    {
        public static List<InventarDTO> MapGrupa(this List<Inventar> inventari)
        {

            var mapper = InventarMapper.InitializeAutomapper();
            var vrati = new List<InventarDTO>();
            inventari.ForEach(g =>
            {
                vrati.Add(mapper.Map<InventarDTO>(g));
            });
            return vrati;
        }
    }
}
