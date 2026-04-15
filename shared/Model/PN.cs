namespace shared.Model;

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
        // TODO: Implement!
        return false;
    }

    public override double doegnDosis() {
    	// TODO: Implement!
	    if (dates.Count() == 0)
		    
	    {
		   return 0;
	    }
	    
	    DateTime tidligst = dates.Min(d => d.dato);
	    DateTime senest = dates.Max(d => d.dato);

	    double antaldage = (senest - tidligst).TotalDays + 1;
	   
	    double resultat = (dates.Count * antalEnheder) / antaldage;
	    return resultat; 
	    
	    
        return -1;
    }


    public override double samletDosis() {
        return dates.Count() * antalEnheder;
    }

    public int getAntalGangeGivet() {
        return dates.Count();
    }

	public override String getType() {
		return "PN";
	}
}
