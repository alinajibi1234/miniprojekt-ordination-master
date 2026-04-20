namespace ordination_test;

using Microsoft.EntityFrameworkCore;

using Service;
using Data;
using shared.Model;

[TestClass]
public class ServiceTest
{
    private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void PatientsExist()
    {
        Assert.IsNotNull(service.GetPatienter());
    }

    [TestMethod]
    public void OpretDagligFast()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.AreEqual(1, service.GetDagligFaste().Count());
    }
    
    [TestMethod]
    public void OpretDagligSkaev()
    {
      
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();
    
        // Vi gemmer antallet før vi opretter for at kunne sammenligne
        int antalFoer = service.GetDagligSkæve().Count();

        // Vi laver nogle test-doser (f.eks. to tidspunkter på døgnet)
        // Bemærk: Din DataService metode kræver måske en liste af 'Dosis' objekter
        DateTime startDato = DateTime.Now;
        DateTime slutDato = DateTime.Now.AddDays(3);

        // 2. Act
        // Her kalder vi din service. Husk at tjekke din metodes signatur i DataService.
        // Typisk sender man en liste af doser med.
        service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId, 
            new List<Dosis> { 
                new Dosis(DateTime.Now.Date.AddHours(8), 2), // kl. 08:00, 2 enheder
                new Dosis(DateTime.Now.Date.AddHours(20), 1) // kl. 20:00, 1 enhed
            }.ToArray(), 
            startDato, slutDato);

        // 3. Assert
        // Vi tjekker om listen er blevet én længere
        Assert.AreEqual(antalFoer + 1, service.GetDagligSkæve().Count());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestAtKodenSmiderEnException()
    {
        // Herunder skal man så kalde noget kode,
        // der smider en exception.
        int ugyldigtPatientId = 999;
        int laegemiddelId = 1;
        
        // Hvis koden _ikke_ smider en exception,
        // så fejler testen.
        service.GetAnbefaletDosisPerDøgn(ugyldigtPatientId, laegemiddelId);
        
        Console.WriteLine("Her kommer der ikke en exception. Testen fejler.");
    }
}