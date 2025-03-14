﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosPollos.Domain.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        IDishRepository dishRepository { get; }        
        IRestaurantRepository restaurantRepository { get; }
        Task Save();
    }
}
