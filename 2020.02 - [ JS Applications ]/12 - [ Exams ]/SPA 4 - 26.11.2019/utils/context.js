export default function (context) {
    firebase.auth().onAuthStateChanged(function (user) {
        if (user) {
            if (context.y) {
                context.loggedInWithNoTreks = false;
                context.loggedInWithTreks = true;
                
            } else {
                context.loggedInWithNoTreks = true;
                context.loggedInWithTreks = false;
            }
            
            if (context.params.xId) {
                context.xId = context.params.xId;
                localStorage.setItem("xId", context.params.xId);
            }
            context.isLoggedIn = true;
            context.userId = user.uid;
            context.username = user.email;
            localStorage.setItem("userId", user.uid);
            localStorage.setItem("userEmail", user.email);
        } else {
            localStorage.removeItem("userId");
            localStorage.removeItem("userEmail");
            localStorage.removeItem("xId");
        }
    });

    return context.loadPartials({
        header: "../views/common/header.hbs",
        footer: "../views/common/footer.hbs"
    });
}