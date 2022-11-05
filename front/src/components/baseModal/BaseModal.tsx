import { Box, Button, Stack, Typography } from '@mui/material';
import Modal from '@mui/material/Modal';
import { modalBaseStyle } from '@styles/globalStyles';

export function BaseModal({ open, handleClose, title, btnText, body, bodyContainerStyle, btnAction }: any) {
  return (
    <Box>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={{ ...modalBaseStyle, justifyContent: 'space-between', minHeight: '300px' }} component="form">
          <Typography variant="h4" textAlign="center">
            {title}
          </Typography>
          <Box
            sx={{
              padding: '1rem',
              display: 'flex',
              justifyContent: 'center',
              alignContent: 'center',
              width: '100%',
              ...bodyContainerStyle,
            }}
          >
            {body}
          </Box>
          <Stack direction="row" justifyContent="center" spacing={2} sx={{ width: '100%' }}>
            {btnText && (
              <Button onClick={btnAction} variant="contained" color="error" fullWidth>
                {btnText}
              </Button>
            )}
            <Button onClick={handleClose} variant="contained" fullWidth>
              Close
            </Button>
          </Stack>
        </Box>
      </Modal>
    </Box>
  );
}
