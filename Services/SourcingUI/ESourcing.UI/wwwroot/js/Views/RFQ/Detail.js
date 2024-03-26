/*import { signalR } from "../../../lib/microsoft-signalr/signalr";*/

var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5005/auctionhub").build();
var auctionId = document.getElementById("AuctionId").value;

document.getElementById("SendButton").disable = true;

var groupName = "auction- " + auctionId

connection.start().then(function () {

    document.getElementById("SendButton").disable = false;
    connection.invoke("AddToGroupAsync", groupName).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
})

document.getElementById("SendButton").addEventListener("click", function (event) {
    var user = document.getElementById("SellerUserName").value;
    var productId = document.getElementById("ProductId").value;
    var sellerUser = user;
    var bid = document.getElementById("exampleInputPrice").value;
    var sendBidRequest = {
        AuctionId: auctionId,
        ProductId: productId,
        SellerUserName: sellerUser,
        Price: parseFloat(bid).toString()
    }

    SendBid(sendBidRequest);
    event.preventDefault();
});

function SendBid(model) {

}
