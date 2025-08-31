using System;
using System.Collections.Generic;

namespace MoneyManagerApp.Presentation.Models;

public partial class Transaction
{
    public int TransactionsId { get; set; }

    public int? TransactionsType { get; set; }

    public int? FkAccountsIdFrom { get; set; }

    public int? FkAccountsIdTo { get; set; }

    public string? TransactionsDescription { get; set; }

    public decimal TransactionsSum { get; set; }

    public DateTime TransactionsDate { get; set; }

    public virtual Account? FkAccountsIdFromNavigation { get; set; }

    public virtual Account? FkAccountsIdToNavigation { get; set; }
}
