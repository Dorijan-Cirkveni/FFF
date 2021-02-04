import React from 'react';
import PropTypes from 'prop-types';

import Paper from '@material-ui/core/Paper';
import useStyles from "../../styles/pagewrapper"


function PageWrapper(props) {
    const { children, ...other } = props;
    const classes = useStyles();
    return (
        <div className={classes.root}>
            <div className={classes.header}></div>
            <Paper elevation={3} className={classes.paper}>
                <div className={classes.content}>
                    {children}
                </div>
            </Paper>
        </div>
    );
}
PageWrapper.propTypes = {
    children: PropTypes.node
};

export default PageWrapper;