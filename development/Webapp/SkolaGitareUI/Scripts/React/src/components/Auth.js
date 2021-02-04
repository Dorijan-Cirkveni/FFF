import React, { Component } from "react";
import axios from 'axios';
import jwt_decode from "jwt-decode";

import { AuthProvider } from "../authContext";





class Auth extends Component {
    state = {
        authenticated: false,
        user: {
            role: "visitor",
            Id: ""
        },
        accessToken: "",
    };

    initiateLogin = (values) => {
        const auth = axios.create({
            baseURL: 'https://localhost:5001/api/',
           
        });
        auth.post('User/Login', {
            "username": "teacher@teacher.com",
            "password": "$Teacher1$"
        }).then(resp => {
            let decoded = jwt_decode(resp.data.token);
            this.setSession(resp.data.token,
                decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
                decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']);
        }).catch(err => {
            console.log(err);
        }


        );
       
               

    };


    logout = () => {
        this.setState({
            authenticated: false,
            user: {
                role: "visitor",
                Id: ""
            },
            accessToken: "",
        });
    };

    

    setSession(accessToken, role, id) {
        console.log(accessToken);
        this.setState({
            authenticated: true,
            accessToken: accessToken,
            user: {
                role: role,
                Id: id
            },
        });

    }
    

    render() {
        const authProviderValue = {
            ...this.state,
            initiateLogin: this.initiateLogin,
            handleAuthentication: this.handleAuthentication,
            logout: this.logout
        };

        return (
            <AuthProvider value={authProviderValue}>
                {this.props.children}
            </AuthProvider>
        );
    }
}

export default Auth;