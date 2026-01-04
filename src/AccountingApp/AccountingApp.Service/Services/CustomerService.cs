using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.DTOS;
using AccountingApp.Core.Entities;
using AccountingApp.Core.Exceptions;
using AccountingApp.Core.Interfaces;
using AccountingApp.Services.Interfaces;
using AutoMapper;


namespace AccountingApp.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerDto> CreateAsync(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task DeleteAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer == null)
            {
                throw new NotFoundException("Silinecek müşteri bulunamadı.");
            }

            await _unitOfWork.Customers.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();

            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (customer == null)
            {
                throw new NotFoundException("Müşteri bulunamadı.");
            }

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task UpdateAsync(int id, CustomerDto customerDto)
        {
            var existingCustomer = await _unitOfWork.Customers.GetByIdAsync(id);
            if (existingCustomer == null)
            {
                throw new NotFoundException("Güncellenecek müşteri bulunamadı.");
            }

            _mapper.Map(customerDto, existingCustomer);

            await _unitOfWork.Customers.UpdateAsync(existingCustomer);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
