namespace shared.Model;
using System.Linq;

public class PN : Ordination {
	public double antalEnheder { get; set; }
    public List<Dato> dates { get; set; } = new List<Dato>();

    public PN (DateTime startDen, DateTime slutDen, double antalEnheder, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen) {
		this.antalEnheder = antalEnheder;
	}

    public PN() : base(null!, new DateTime(), new DateTime()) {
    }

    /// <summary>
    /// Registrerer at der er givet en dosis på dagen givesDen
    /// Returnerer true hvis givesDen er inden for ordinationens gyldighedsperiode og datoen huskes
    /// Returner false ellers og datoen givesDen ignoreres
    /// </summary>
    public bool givDosis(Dato givesDen) {
	    if (givesDen.dato.Date >= startDen.Date && givesDen.dato.Date <= slutDen.Date) {
		    dates.Add(givesDen);
		    return true;
	    }

	    return false;
    }

    public override double doegnDosis() {
	    if (dates.Count == 0) {
		    return 0;
	    }

	    DateTime minDate = dates.Min(d => d.dato).Date;
	    DateTime maxDate = dates.Max(d => d.dato).Date;
	    
	    int dage = (maxDate - minDate).Days +1;

	    return samletDosis() / dage;
    }


    public override double samletDosis() {
        return dates.Count * antalEnheder;
    }

    public int getAntalGangeGivet() {
        return dates.Count;
    }

	public override String getType() {
		return "PN";
	}
}
