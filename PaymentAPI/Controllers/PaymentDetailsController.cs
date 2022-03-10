using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly PaymentDetailContext _context;
        public PaymentDetailsController(PaymentDetailContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentDetails>> GetpaymentDetails()
        {
            return await _context.paymentDetails.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetails>> GetPaymentDetail(int id)
        {
            var paymentDetail = await _context.paymentDetails.FindAsync(id);
            if(paymentDetail == null)
            {
                return NotFound();
            }
            return paymentDetail;
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDetails>> PostPaymentDetail(PaymentDetails paymentDetails)
        {
            _context.paymentDetails.Add(paymentDetails);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPaymentDetail", new { id = paymentDetails.PaymentDetailId, paymentDetails });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentDetails>> PutPaymentDetail(int id, PaymentDetails paymentDetails)
        {
            if(id != paymentDetails.PaymentDetailId)
            {
                return BadRequest();
            }
            
            _context.Entry(paymentDetails).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(id))
                {
                    return NotFound();
                }
                else
                {

                throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDetails>> DeletePaymentDetail(int id)
        {
            var paymentDetail = await _context.paymentDetails.FindAsync(id);
            if(paymentDetail == null)
            {
                return NotFound();
            }
            _context.paymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentDetailExists(int id)
        {
            return _context.paymentDetails.Any(a => a.PaymentDetailId == id);
        }


    }
}
