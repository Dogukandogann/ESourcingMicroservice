<h1>Online Müzayede Sitesi</h1>
<h2>Açıklama</h2>
<p>Bu proje, kullanıcıların alıcı ve satıcı olarak siteye kayıt olabildiği bir online müzayede sitesini içerir. Satıcılar ürünlerini müzayede yöntemi ile satışa çıkarabilir, alıcılar ise müzayede süresi sonlanmamış ürünlere gerçek zamanlı teklif vererek ve diğer kullanıcıların tekliflerini görerek satın alma işlemi gerçekleştirebilirler.Proje, Docker üzerinden ayağa kaldırılmış altı ana container üzerinden çalışmaktadır.
</p>

<h2>Servisler</h2>

<h3>Product Servis</h3>
<p>Ürünleri barındıran ve MongoDB kullanan API servisidir.</p>

<h3>Sourcing Servis</h3>
<p>İhalelerin kontrolünü sağlayan, MongoDB ve SignalR kullanan API servisidir.</p>

<h3>EventBus Servis</h3>
<p>Etkinlikleri ileten bir aracıdır. Servis, bir dizi mikro servis arasında iletişim sağlamak için kullanılır.</p>

<h3>Order Servis</h3>
<p>Sourcing servisden gelen etkinlikleri tüketen ve MS SQL veritabanı kullanan API servisidir.</p>

<h3>API Gateway Servis</h3>
<p>Gelen istekleri yönlendiren, yetkilendiren, güvenlik politikalarını uygulayan, yük dengelemesi yapan ve uygun mikro servislere yönlendiren Ocelot API servisidir.</p>

<h3>UI Servis</h3>
<p>Kullanıcıların basit bir arayüz ile müzayede sistemine katılma veya ürün satış yapmalarına olanak tanıyan, kullanıcıların kayıt, giriş ve yetkilendirme işlemlerini kontrol eden, MS SQL veritabanı kullanan MVC servisidir.</p>


<h2>Teknolojiler</h2>
<ul>
  <li>Geliştirme Ortamı: Visual Studio 2022
</li>
  <li>Kod Altyapısı: C#, ASP.NET MVC, .NET CORE 7,Web Api</li>
  <li>Mimari Tasarım: N-Tier Katmanlı Mimari</li>
  <li>Veri Tabanı: Ms SQL Server,Mongo Db</li>
  <li>Veri Erişim Teknolojisi: Entity Framework CORE (CodeFirst) , Linq</li>
  <li>Arayüz Tasarımı: HTML5, CSS3, Javascript, JQuery, Ajax, Bootstrap</li>
  <li>Diğer Özellikler: DTO, ViewModel, AutoMapper, FluentValidation, Microsoft Identity,Partial Views, View Components, Repository Design Pattern , Dependency Injection,Rabbit MQ Library,CQRS,MediatR design Pattern,Event Sourcing,SignalR,Docker - compose yaml,</li>
  <li>Versiyon Kontrol Sistemi: Git</li>
  <li>Altyapı ve Dağıtım Araçları:Docker,Docker Compose</li>
</ul>

<h1>English</h1>

<h1>Online Auction Site</h1>
<h2>Description</h2>
<p>This project contains an online auction site where users can register as buyers or sellers. Sellers can list their products for auction, while buyers can place real-time bids on products with ongoing auctions and see other users' bids to complete the purchase process. The project runs on six main containers deployed via Docker.</p>

<h2>Services</h2>

<h3>Product Service</h3>
<p>An API service that hosts products and uses MongoDB.</p>

<h3>Sourcing Service</h3>
<p>An API service that manages auctions, utilizing MongoDB and SignalR.</p>

<h3>EventBus Service</h3>
<p>An intermediary service that transmits events between microservices to enable communication.</p>

<h3>Order Service</h3>
<p>An API service that consumes events from the sourcing service and uses MS SQL Server for data storage.</p>

<h3>API Gateway Service</h3>
<p>An Ocelot API service that routes requests, authorizes, applies security policies, load balances, and directs requests to appropriate microservices.</p>

<h3>UI Service</h3>
<p>An MVC service with a simple interface allowing users to participate in auctions or sell products. It manages user registration, login, and authorization processes, utilizing MS SQL Server for data storage.</p>


<h2>Technologies</h2>
<ul>
  <li>Development Environment: Visual Studio 2022</li>
  <li>Framework: C#, ASP.NET MVC, .NET Core 7, Web API</li>
  <li>Architectural Design: N-Tier Layered Architecture</li>
  <li>Database: MS SQL Server, MongoDB</li>
  <li>Data Access Technology: Entity Framework Core (CodeFirst), LINQ</li>
  <li>UI Design: HTML5, CSS3, JavaScript, jQuery, Ajax, Bootstrap</li>
  <li>Other Features: DTO, ViewModel, AutoMapper, FluentValidation, Microsoft Identity, Partial Views, View Components, Repository Design Pattern, Dependency Injection, RabbitMQ Library, CQRS, MediatR Design Pattern, Event Sourcing, SignalR, Docker - Compose YAML</li>
  <li>Version Control System: Git</li>
  <li>Infrastructure and Deployment Tools: Docker, Docker Compose</li>
</ul>

<h2>Images</h2>
<h3>Architectural Structure</h3>
<img src="https://github.com/Dogukandogann/ESourcingMicroservice/assets/134203440/fa25dccc-b62d-48cf-a3ff-9843fa9141f9" alt="1" style="max-width: 100%;">
<h3>Real-Time Bidding</h3>
<img src="https://github.com/Dogukandogann/ESourcingMicroservice/assets/134203440/3ce0b45a-ce68-4928-837a-adaf61fa3acd" alt="1" style="max-width: 100%;">
<h3>Auction Status Control Screen</h3>
<img src="https://github.com/Dogukandogann/ESourcingMicroservice/assets/134203440/9f41817c-dae7-43d6-83de-017a331092cf" alt="1" style="max-width: 100%;">
