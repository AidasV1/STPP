import AuthService from "../services/auth.service";

function Navbar() {
    const currentUser = AuthService.getCurrentUser();

    function logOut() {
        AuthService.logout();
        this.setState({
            currentUser: undefined,
        });
    }

	let html =
        <nav className="light-blue lighten-1" role="navigation">
            <div className="nav-wrapper container">
                <a id="logo-container" href="/" className="material-icons">home</a>
                {currentUser ? (
                    <ul id="nav-mobile" className="right hide-on-med-and-down">
                        <li><a id="listing-create" href="/api/CityCreate">Pridėti miestą</a></li>
                        <li><a id="login-container" href="/api/login" onClick={logOut}>Atsijungti</a></li>
                    </ul>
                ) : (
                    <ul id="nav-mobile" className="right hide-on-med-and-down">
                        <li><a id="login-container" href="/api/login">Prisijungti</a></li>
                    </ul>
                )}
                {/* <ul id="nav-mobile" className="right hide-on-med-and-down">
                    <li><a id="listing-create" href="/api/CityCreate">Pridėti miestą</a></li>
                    <li><a id="login-container" href="/api/login">Prisijungti</a></li>
                </ul> */}
            </div>
        </nav>

	return html;
}

export default Navbar;