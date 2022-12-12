import React, { Component } from "react";
import AuthService from "../services/auth.service";
import { withRouter } from './common/with-router';
import { TextInput } from 'react-materialize';
import 'materialize-css/dist/css/materialize.css'
import { replaceBackend } from "../components/backend/backend.tsx";
import axios from "axios";
import Form from "react-validation/build/form";
import CheckButton from "react-validation/build/button";

const required = value => {
  if (!value) {
    return (
      <div className="alert alert-danger" role="alert">
        This field is required!
      </div>
    );
  }
};

class Login extends Component {
  constructor(props) {
    super(props);
    this.handleLogin = this.handleLogin.bind(this);
    this.onChangeUsername = this.onChangeUsername.bind(this);
    this.onChangePassword = this.onChangePassword.bind(this);

    this.state = {
      username: "",
      password: "",
      loading: false,
      message: ""
    };
  }

  onChangeUsername(e) {
    this.setState({
      username: e.target.value
    });
  }

  onChangePassword(e) {
    this.setState({
      password: e.target.value
    });
  }

  handleLogin(e) {
    e.preventDefault();

    this.setState({
      message: "",
      loading: true
    });

    this.form.validateAll();

    if (this.checkBtn.context._errors.length === 0) {
        AuthService.login(this.state.username, this.state.password).then(
          () => {
            this.props.router.navigate("/");
            //   window.location.reload();
            const a = AuthService.getCurrentUser();
            let backendWithAuth = axios.create({
              headers: {
                Authorization: `Bearer ${a.accessToken}`,
              },
            });
            replaceBackend(backendWithAuth);
          },
          (error) => {
            const resMessage =
              (error.response &&
                error.response.data &&
                error.response.data.message) ||
              error.message ||
              error.toString();
  
            this.setState({
              loading: false,
              message: resMessage,
            });
          }
        );
      } else {
        this.setState({
          loading: false,
        });
      }
  }

  render() {
    return (
        <div className="container">
            <div className="section">

                <center>
                    <h5 className="blue-text">Prisijunkite prie savo paskyros</h5>
                    <div className="section"></div>

                    <Form
                        onSubmit={this.handleLogin}
                        ref={c => {
                            this.form = c;
                        }}
                    >
                        <div className="container">
                            <div className="login-register-page z-depth-1 grey lighten-4 row">
                                    <div className='row'>
                                        <div className='col s12'></div>
                                    </div>

                                    <div className='row'>
                                        <div className='input-field col s12'>
                                            <TextInput
                                                id="usernameInput"
                                                label="Vartotojo vardas"
                                                value={this.state.username}
                                                onChange={this.onChangeUsername}
                                                validations={[required]}
                                            />
                                        </div>
                                    </div>

                                    <div className='row'>
                                        <div className='input-field col s12'>
                                            <TextInput
                                                id="passwordInput"
                                                label="Slaptažodis"
                                                password
                                                value={this.state.password}
                                                onChange={this.onChangePassword}
                                                validations={[required]}
                                            />
                                        </div>
                                    </div>

                                    <div className="form-group">
                                        <button
                                            className="btn btn-success btn-block"
                                            disabled={this.state.loading}
                                        >
                                            {this.state.loading && (
                                                <span className="spinner-border spinner-border-sm"></span>
                                            )}
                                            <span>Prisijungti</span>
                                        </button>
                                    </div>

                                    {this.state.message && (
                                        <div className="form-group">
                                            <div className="alert alert-danger" role="alert">
                                                {this.state.message}
                                            </div>
                                        </div>
                                    )}
                                    <CheckButton
                                        style={{ display: "none" }}
                                        ref={(c) => {
                                            this.checkBtn = c;
                                        }}
                                    />
                            </div>
                        </div>
                    </Form>
                    <a href="Register">Kurti paskyrą</a>
                </center>

            </div>
            <br/><br/>
        </div>
    );
  }
}

export default withRouter(Login);