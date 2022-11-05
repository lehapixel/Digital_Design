function getWeather() {
	let city = (document.getElementById("city").value).toLowerCase();
	if (city == "") {
		alert("Введите название города (латиница)");
	} 
	else {
		let apiKey = '6e108f31f8c7b87f112ff4a50417dcec';
		var url = 'http://api.openweathermap.org/data/2.5/weather?q=' + city + ',RU&appid=' + apiKey + '&units=metric';
		console.log(url);
		var request = new XMLHttpRequest();
		request.open('GET', url, true);
		request.onload = function () {
			if (request.status >= 200 && request.status < 400) {
				var data = JSON.parse(request.responseText);
				console.log(data);
			} 
			else {
				console.log('Сервер вернул ошибку');
			}
		};
		request.onerror = function () {
			console.log('Ошибка подключения');
		};
		request.send();
		request.onload = function () {
			var data = JSON.parse(request.responseText);
			document.getElementById('weather').innerHTML = data.weather[0].description;
			document.getElementById("weather-image").innerHTML = `<img src="http://openweathermap.org/img/w/${data.weather[0].icon}.png">`;
			document.getElementById('temp').innerHTML = data.main.temp + "°C";
			document.getElementById('humidity').innerHTML = "Влажность: "+ data.main.humidity + "%";
		}
	}
}