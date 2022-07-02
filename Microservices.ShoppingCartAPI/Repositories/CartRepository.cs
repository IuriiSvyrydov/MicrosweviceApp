using AutoMapper;
using Microservices.ShoppingCartAPI.Data;
using Microservices.ShoppingCartAPI.Dto;
using Microservices.ShoppingCartAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservices.ShoppingCartAPI.Repositories
{
#pragma warning disable
    public class CartRepository: ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public CartRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            var cart = new Cart
            {
                CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId)
            };
            cart.CartDetails = _context.CartDetails.Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId)
                .Include(p=>p.Product);
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);
            var productInDb = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == cartDto.CartDetails.FirstOrDefault().ProductId);
            if (productInDb is null)
            {
                _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _context.SaveChangesAsync();
            }

            var cartHeaderFromDb = await _context.CartHeaders.AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);
            if (cartHeaderFromDb==null)
            {
                _context.CartHeaders.Add(cart.CartHeader);
                await _context.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null; 
                _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _context.SaveChangesAsync();

            }
            else
            {
                //check details about product

                var checkDetails = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                         u.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                if (checkDetails==null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();

                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Product = null;

                    cart.CartDetails.FirstOrDefault().Count += checkDetails.Count;
                    _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
            }

            return _mapper.Map<CartDto>(cart); 

        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            try
            {
                var cartDetails = await _context.CartDetails.AsNoTracking()
                    .FirstOrDefaultAsync(u=>u.CartDetailsId== cartDetailsId);
                int totalCountOfCartItem =  _context.CartDetails
                    .Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();
                _context.CartDetails.Remove(cartDetails);
                if (totalCountOfCartItem==1)
                {
                    var cartHeaderToRemove = await _context.CartHeaders
                        .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);
                    _context.CartHeaders.Remove(cartHeaderToRemove);
               
                }

                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }


        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartFromDb = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId);
            if (cartFromDb!=null)
            {
                _context.CartDetails.RemoveRange(_context.CartDetails
                    .Where(u=>u.CartHeaderId==cartFromDb.CartHeaderId));
                _context.CartHeaders.Remove(cartFromDb);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
  