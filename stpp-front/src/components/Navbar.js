function Navbar() {
	let html =
        <nav className="light-blue lighten-1" role="navigation">
            <div className="nav-wrapper container">
                <a id="logo-container" href="/" className="material-icons">home</a>
                <ul id="nav-mobile" className="right hide-on-med-and-down">
                    <li><a id="listing-create" href="/ListingCreate">Kurti skelbimą</a></li>
                    <li><a id="login-container" href="/api/login">Prisijungti</a></li>
                </ul>
                {/* <a id="listing-create" href="/">Kurti skelbimą</a>
                <a id="login-container" href="/Login" className="right material-icons">lock_open</a> */}
            </div>
        </nav>

	return html;
}

export default Navbar;