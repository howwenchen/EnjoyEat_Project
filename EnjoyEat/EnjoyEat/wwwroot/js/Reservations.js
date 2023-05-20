let app = new Vue({
    el: "#d1",
    data: {
        test: "ss",
        ReserveDate: "",
    },
});
    //methods: {
    //    disablerange: function (date) {
    //        var today = new Date();
    //        today.setHours(0, 0, 0, 0);
    //        var mindate = new Date(today.getTime() + 1 * 24 * 3600 * 1000);
    //        var maxdate = new Date(today.getTime() + 30 * 24 * 3600 * 1000);
    //        var isBooking = this.bookingInfos.some(x => new Date(x.date).getTime() == date.getTime() && x.total >= 14);
    //        return date < mindate || date > maxdate || date.getDay() % 7 == 1 || isBooking;
    //    }
    //}
