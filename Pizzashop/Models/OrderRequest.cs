using System.Text.Json;
using Pizzashop.Enums;

namespace Pizzashop.Models;

public class OrderRequest
{
    public string OrderType { get; set; } = ""; // TODO - map to enum, validate

    public string Description { get; set; } = "";
    public IList<string>? Items { get; set; }
}