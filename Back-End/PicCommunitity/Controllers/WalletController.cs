using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using PicCommunitity.Models;
using PicCommunitity.Services;
using PicCommunitity.ViewsModels;

namespace PicCommunitity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : Controller
    {
        private AppDbContext context;
        private WalletServices services;
        //private AccountServices vice_services;
        public WalletController(AppDbContext context)
        {
            this.context = context;
            this.services = new WalletServices(context);
        }
        [Route("getWalletInfo")]
        [HttpGet]
        public IActionResult getWalletInfo(string userId)
        {
            var user = context.users.FirstOrDefault(u => u.u_id == userId);
            var wallet = context.wallet.FirstOrDefault(u => u.u_id == userId);
            return Ok(new
            {
                Success = true,
                UserName = user.u_name,
                NowCoin = wallet.coin,
                BuyNum = wallet.buy_num,
                msg = "Operation Done"
            });
        }
        [Route("depositWallet")]
        [HttpPost]
        public IActionResult depositWallet(string userId, int amount)
        {
            var nowWallet = context.wallet.FirstOrDefault(u => u.u_id == userId);
            context.Entry(nowWallet).CurrentValues.SetValues(new wallet
            {
                u_id = nowWallet.u_id,
                coin = nowWallet.coin + amount,
                publish_num = nowWallet.publish_num,
                buy_num = nowWallet.buy_num + 1
            });
            context.SaveChanges();
            //
            var nowPayment = new payment
            {
                u_id = userId,
                pay_time = DateTime.Now,
                coin = amount,
                source_typepicture = "ol"
            };
            context.payment.Add(nowPayment);
            context.SaveChanges();
            return Ok(new
            {
                Success = true,
                msg = "Operation Done"
            });
        }
    }
}
