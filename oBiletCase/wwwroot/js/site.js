// Index'te bulunan Select elementlerini Jquery-Select2 ya çevirir.
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
                    // bulunan sonuçlar uygun þekilde formatlanarak geri dönülür.
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
            delay: 250, // Basic rate-limiting gecikme süresi-istekler arasý beklenecek süre
            minimumInputLength: 1 // arama yapmak için kaç karakter girilmesi gerektiðinin ayarý
        }
    });

};

// Nereden ve Nereye seçimlerinin deðiþikliðini yapar
function swapValues(dep, dest) {
    // Select2 elementlerine yeni deðerler set edilerek deðiþiklik için tetiklenir.
    $('#DepartureCitySelect').val(dest).trigger('change');
    $('#DestinationCitySelect').val(dep).trigger('change');

    // Nereden seçimindeki Þehir ve Id deðeri alýnýr.
    var depCity = $('#DepartureCity').val();
    var depCityId = $('#DepartureCityId').val();

    // Nereye seçimindeki Þehir ve Id deðeri alýnýr.
    var destCity = $('#DestinationCity').val();
    var destCityId = $('#DestinationCityId').val();

    // Deðerler birbirleri ile deðiþtirilir. Departure -> Destination ve Destination -> Departure
    $('#DepartureCity').val(destCity);
    $('#DestinationCity').val(depCity);
    $('#DepartureCityId').val(destCityId);
    $('#DestinationCityId').val(depCityId);
}

// Bugün butonu için oluþturulan function. Bugün deðerini alarak DepartureDate input'una set eder.
function setToday() {
    var dateInput = $('#DepartureDate');
    var today = new Date();
    dateInput.val(today.toISOString().substr(0, 10));
}

// Yarýn butonu için oluþturulan function. Yarýn deðerini alarak DepartureDate input'una set eder.
function setTomorrow() {
    var dateInput = $('#DepartureDate');
    var tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    dateInput.val(tomorrow.toISOString().substr(0, 10));
}

$(document).ready(function () {

    // DepartureCity liste inputu oluþturulur.
    var depCitySelect2 = $('#DepartureCitySelect');
    inizializeSelect2(depCitySelect2);

   // DestinationCity liste inputu oluþturulur.
    var destCitySelect2 = $('#DestinationCitySelect');
    inizializeSelect2(destCitySelect2);

    // DepartureCity listesinde seçim yapýldýðýnda tetiklenir.
    // DestinationCity ile ayný Þehir seçilmiþse deðerler Swap edilir.
    $('#DepartureCitySelect').on('select2:select', function (e) {
        var data = e.params.data;

        var destinationCityId = $('#DestinationCityId').val();
        var departureCityId = $('#DepartureCityId').val();

        // Ayný þehir seçilmiþ ise deðerler swap edilir.
        if (destinationCityId == data.id) {
            swapValues(departureCityId, destinationCityId);
            return;
        } 

        $('#DepartureCity').val(data.text);
        $('#DepartureCityId').val(data.id);
    });

    // DestinationCity listesinde seçim yapýldýðýnda tetiklenir.
    // DepartureCity ile ayný Þehir seçilmiþse deðerler Swap edilir.
    $('#DestinationCitySelect').on('select2:select', function (e) {
        var data = e.params.data;

        var destinationCityId = $('#DestinationCityId').val();
        var departureCityId = $('#DepartureCityId').val();

        // Ayný þehir seçilmiþ ise deðerler swap edilir.
        if (departureCityId == data.id) {
            swapValues(departureCityId, destinationCityId);
            return;
        }

        $('#DestinationCity').val(data.text);
        $('#DestinationCityId').val(data.id);
    });

    // swap button click iþlevi
    $('#swap').click(function (e) {

        var departureCityS = depCitySelect2.val();
        var destinationCityS = destCitySelect2.val();

        swapValues(departureCityS, destinationCityS);

        
    });

});