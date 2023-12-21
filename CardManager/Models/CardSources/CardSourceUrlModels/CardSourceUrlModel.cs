using CardManager.Models.CardSources.CardSourceUrlModels.UrlGetParameters;

namespace CardManager.Models.CardSources.CardSourceUrlModels;

public interface ICardSourceUrlModel
{
    string Url { get; set; }
    ICollection<IUrlGetParameter> UrlGetParameters { get; set; }
}

public class CardSourceUrlModel : ICardSourceUrlModel
{
    public string Url { get; set; } = string.Empty;

    public ICollection<IUrlGetParameter> UrlGetParameters { get; set; } = new List<IUrlGetParameter>();
}

// tcgplayer.com
// pricecharting.com


// mavin.io
// https://mavin.io/search?q=vaporeon+33%2F144&bt=sold
// https://mavin.io/search?q=Vaporeon+33%2f144&bt=sold
// https://mavin.io/search?q=name     number  &bt=sold
//  <a href="#worth" id="worthBox" style="text-decoration: none;">
//      <h5>Worth</h5>
//      <h4>$39.29</h4>
//  </a>
//  <p id="answer">
//      The average value of
//          "<strong>vaporeon 33/144</strong>"
//      is
//          <a href="#worth">$39.29</a>.
//      Sold comparables range in price from a low of
//          <a href="/item/Pokemon-Vaporeon-Skyridge-Rare-33%2F144-MP%21%21?itemId=154678458376&amp;q=vaporeon%2033%2F144"
//              class="worthModalItem default-link"
//              id="lowestWorthItemHeader"
//              target="_blank"
//              name="https://cdn6.mavin.io/production/soldItems/155802818/images/image-0.jpg"
//              data-id="154678458376"
//              alt="Pokemon Vaporeon Skyridge Rare 33/144 MP!!"
//              data-sold="$0.99"
//              data-shipping="$0.00">
//                  $0.99
//          </a>
//      to a high of
//          <a href="/item/PSA-10-GEM-MINT-Vaporeon-33%2F144-Skyridge-REVERSE-HOLO-Pokemon-Card-748?itemId=313802247098&amp;q=vaporeon%2033%2F144"
//              class="worthModalItem default-link"
//              id="highestWorthItemHeader"
//              target="_blank"
//              name="https://cdn3.mavin.io/production/soldItems/157760437/images/image-0.jpg"
//              data-id="313802247098"
//              alt="PSA 10 GEM MINT Vaporeon 33/144 Skyridge REVERSE HOLO Pokemon Card 748"
//              data-sold="$355.00"
//              data-shipping="$0.00">
//                  $355.00
//          </a>
//      .
//  </p>