﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace OrderService.Database.Entities;

public partial class OrderState
{
    public Guid CorrelationId { get; set; }

    public string CurrentState { get; set; }

    public DateTime? OrderCreationDateTime { get; set; }

    public DateTime? OrderCancelDateTime { get; set; }

    public DateTime? OrderAcceptDateTime { get; set; }

    public Guid? OrderId { get; set; }

    public decimal? Price { get; set; }

    public string Product { get; set; }
}