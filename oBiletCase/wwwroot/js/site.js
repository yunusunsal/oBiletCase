// Index'te bulunan Select elementlerini Jquery-Select2 ya �evirir.
function inizializeSelect2(el) {
    el.select2({
        width: '100%',
        placeholder: "Select a location",
        ajax: {
            url: 'BusSearch/Search',
            data: function (params) {
                return {
                    searchText: params.term //aranan kelime
                };
            },
            dataType: "json",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            processResults: function (data, search) {
                return {
                    // bulunan sonu�lar uygun �ekilde formatlanarak geri d�n�l�r.
                    results: $.map(data, function (item) {
                        return {
                            text: item.name,
                            id: item.id
                        };
                    })
                };
            },
            error: function (jqXHR, status, error) {
                console.log("Please try again later!");
                alert("Please try again later!");
                return { results: [] }; // Return empty dataset to load after error
            },
            delay: 250, // Basic rate-limiting gecikme s�resi-istekler aras� beklenecek s�re
            minimumInputLength: 1 // arama yapmak i�in ka� karakter girilmesi gerekti�inin ayar�
        }
    });

};

// Nereden ve Nereye se�imlerinin de�i�ikli�ini yapar
function swapValues(dep, dest) {
    // Select2 elementlerine yeni de�erler set edilerek de�i�iklik i�in tetiklenir.
    $('#DepartureCitySelect').val(dest).trigger('change');
    $('#DestinationCitySelect').val(dep).trigger('change');

    // Nereden se�imindeki �ehir ve Id de�eri al�n�r.
    var depCity = $('#DepartureCity').val();
    var depCityId = $('#DepartureCityId').val();

    // Nereye se�imindeki �ehir ve Id de�eri al�n�r.
    var destCity = $('#DestinationCity').val();
    var destCityId = $('#DestinationCityId').val();

    // De�erler birbirleri ile de�i�tirilir. Departure -> Destination ve Destination -> Departure
    $('#DepartureCity').val(destCity);
    $('#DestinationCity').val(depCity);
    $('#DepartureCityId').val(destCityId);
    $('#DestinationCityId').val(depCityId);
}

// Bug�n butonu i�in olu�turulan function. Bug�n de�erini alarak DepartureDate input'una set eder.
function setToday() {
    var dateInput = $('#DepartureDate');
    var today = new Date();
    dateInput.val(today.toISOString().substr(0, 10));
}

// Yar�n butonu i�in olu�turulan function. Yar�n de�erini alarak DepartureDate input'una set eder.
function setTomorrow() {
    var dateInput = $('#DepartureDate');
    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    dateInput.val(tomorrow.toISOString().substr(0, 10));
}

$(document).ready(function () {

    // DepartureCity liste inputu olu�turulur.
    var depCitySelect2 = $('#DepartureCitySelect');
    inizializeSelect2(depCitySelect2);

   // DestinationCity liste inputu olu�turulur.
    var destCitySelect2 = $('#DestinationCitySelect');
    inizializeSelect2(destCitySelect2);

    // DepartureCity listesinde se�im yap�ld���nda tetiklenir.
    // DestinationCity ile ayn� �ehir se�ilmi�se de�erler Swap edilir.
    $('#DepartureCitySelect').on('select2:select', function (e) {
        var data = e.params.data;

        var destinationCityId = $('#DestinationCityId').val();
        var departureCityId = $('#DepartureCityId').val();

        // Ayn� �ehir se�ilmi� ise de�erler swap edilir.
        if (destinationCityId == data.id) {
            swapValues(departureCityId, destinationCityId);
            return;
        } 

        $('#DepartureCity').val(data.text);
        $('#DepartureCityId').val(data.id);
    });

    // DestinationCity listesinde se�im yap�ld���nda tetiklenir.
    // DepartureCity ile ayn� �ehir se�ilmi�se de�erler Swap edilir.
    $('#DestinationCitySelect').on('select2:select', function (e) {
        var data = e.params.data;

        var destinationCityId = $('#DestinationCityId').val();
        var departureCityId = $('#DepartureCityId').val();

        // Ayn� �ehir se�ilmi� ise de�erler swap edilir.
        if (departureCityId == data.id) {
            swapValues(departureCityId, destinationCityId);
            return;
        }

        $('#DestinationCity').val(data.text);
        $('#DestinationCityId').val(data.id);
    });

    // swap button click i�levi
    $('#swap').click(function (e) {

        var departureCityS = depCitySelect2.val();
        var destinationCityS = destCitySelect2.val();

        swapValues(departureCityS, destinationCityS);

        
    });

});