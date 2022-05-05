using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMSBusinessLayer.ContractsBLL;
using ASMSDataAccessLayer.ContractsDAL;
using ASMSEntityLayer.Models;
using ASMSEntityLayer.ResultModels;
using ASMSEntityLayer.ViewModels;
using AutoMapper;

namespace ASMSBusinessLayer.ImplementationsDAL
{
    public class NeighbourhoodBusinessEngine : INeighbourhoodBusinessEngine
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;

        public NeighbourhoodBusinessEngine(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitofWork = unitOfWork;
            _mapper = mapper;
        }
        public IDataResult<ICollection<NeighbourhoodVM>> GetNeighbourhoodsOfDistrict(int districtId)
        {
            try
            {
                if (districtId>0)
                {
                    var neighbourhoods = _unitofWork.NeighbourhoodRepo.GetAll(x => x.DistrictId == districtId);
                    ICollection<NeighbourhoodVM> result = _mapper.Map<IQueryable<Neighbourhood>, ICollection<NeighbourhoodVM>>(neighbourhoods);
                }
                else
                {
                    throw new Exception("DistrictId düzgün gelmedi.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
