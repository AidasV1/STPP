import 'materialize-css/dist/css/materialize.css'

function Footer() {
	let html =
        <footer className="page-footer blue">
            <div className="container">
                <div className="row">
                    <div className="col l6 s12">
                        <h5 className="white-text">Apie mus</h5>
                        <p className="grey-text text-lighten-4">Torquent ante tellus. Viverra nunc ultrices. Praesent nisl nonummy magna integer. Consectetuer nibh mattis. Sociosqu felis sociosqu. Sociis netus aptent sit dictum. Eleifend nec varius.</p>
                    </div>
                    <div className="col l3 s12">
                        <h5> <a href="#" className="white-text">Kontaktai</a></h5>
                    </div>
                </div>
            </div>
        </footer>

	return html;
}

export default Footer;