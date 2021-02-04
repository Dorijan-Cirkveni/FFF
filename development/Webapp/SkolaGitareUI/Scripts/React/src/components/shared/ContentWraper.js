import React from 'react';
import PropTypes from 'prop-types';
import Grid from "@material-ui/core/Grid";
import Paper from '@material-ui/core/Paper';
import useStyles from "../../styles/ContentWrapper"


function ContentWrapper(props) {
    const { children, title, ...other } = props;
    const classes = useStyles();
    return (
        <div className={classes.gridRoot}>
            <Grid container spacing={0}>
                {title && <Grid item xs={12}>
                    <Paper className={classes.paper}>
                        <h4 className={classes.title}>{title}</h4>
                    </Paper>
                </Grid>}

                <Grid item xs={12} className={classes.contentGrid}>
                    {children}
                </Grid>
            </Grid>
        </div>
    );
}
ContentWrapper.propTypes = {
    children: PropTypes.node,
    title: PropTypes.any.isRequired,
};

export default ContentWrapper;